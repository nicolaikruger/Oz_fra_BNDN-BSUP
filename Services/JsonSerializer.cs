﻿using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RentIt.Services
{
    public class JsonSerializer
    {
        private readonly Helper _h;

        public JsonSerializer(Helper helper)
        {
            _h = helper;
        }

        // The types this JsonSerializer is able to handle
        private readonly IEnumerable<Type> _types = new Type[]{
                                                        typeof(TokenData),
                                                        typeof(AccountData),
                                                        typeof(ProductData),
                                                        typeof(PriceData),
                                                        typeof(RatingData),
                                                        typeof(MetaData),
                                                        typeof(CreditsData),
                                                        typeof(PurchaseData),
                                                        typeof(IdData),
                                                };

        // Converts an object to JSON format, leaving any null value out
        public Stream Json<T>(T obj)
        {
            return asStream(JsonString(obj));
        }

        // Converts an object to JSON format, leaving any null value out. Returns the result as string.
        public string JsonString<T>(T obj)
        {
            var something = obj.GetType();
            if (!_types.Contains(obj.GetType())) throw new Exception("Could not serialize given object - its class is not supported.");

            // Collect all properties to serialize to JSOn
            var properties = new Dictionary<string, string>();
            foreach (var p in obj.GetType().GetProperties())
            {

                object value = p.GetValue(obj);

                if (value == null) continue; // Ignore null values

                // Convert each value to JSON representation
                if (typeof(String).IsInstanceOfType(value)) properties[p.Name] = escape((string)value);
                else if (typeof(uint?).IsInstanceOfType(value)) properties[p.Name] = ((uint?)value).ToString();
                else if (typeof(uint).IsInstanceOfType(value)) properties[p.Name] = ((uint)value).ToString();
                else if (typeof(int).IsInstanceOfType(value)) properties[p.Name] = ((int)value).ToString();
                else if (typeof(bool?).IsInstanceOfType(value)) properties[p.Name] = ((bool) value) ? "true" : "false";
                else if (typeof(bool).IsInstanceOfType(value)) properties[p.Name] = ((bool) value) ? "true" : "false";
                else properties[p.Name] = JsonString(value);
            }

            // Produce JSON

            string[] result = new string[properties.Count];

            int c = 0;
            foreach (var kv in properties)
            {
                result[c++] = "\"" + kv.Key + "\":" + kv.Value;
            }

            return "{" + _h.Join(result, ",") + "}";
        }

        // Converts an object to JSON format, only including those properties which have been specified to be included
        public Stream Json<T>(T obj, string[] keep)
        {
            var keepSet = new HashSet<string>(keep);

            return Json(nullOutBut(obj, keepSet));
        }

        // Converts multiple objects to JSON format
        public Stream Json<T>(T[] objects)
        {
            var values = _h.Map(objects, o => JsonString(o));

            return asStream("[" + _h.Join(values, ",") + "]");
        }

        // Converts multiple objects to JSON format, only including those properties which have been specified to be included
        public Stream Json<T>(T[] objects, string[] keep)
        {
            var keepSet = new HashSet<string>(keep);

            var values = _h.Map(objects, o => JsonString(nullOutBut(o, keepSet)));

            return asStream("[" + _h.Join(values, ",") + "]");
        }

        public Stream Json(string[] strings) 
        {
            return asStream("[" + _h.Join(_h.Map(strings, s => escape(s)), ",") + "]");
        }

        public Stream Json(List<UInt32> ints)
        {
            var list = ints.ToArray();
            return asStream("[" + _h.Join(list, ",") + "]");
        }

        public Stream Json(uint[] intArray)
        {
            return asStream("[" + _h.Join(intArray, ",") + "]");
        }

        // Causes any property but those specified by keep to be set to null
        private T nullOutBut<T>(T obj, HashSet<string> keep)
        {
            foreach (var p in obj.GetType().GetProperties()) {

                if (!keep.Contains(p.Name)) p.SetValue(obj, null);
            }

            return obj;
        }

        // Escapes a string, producing a string literal based on it
        private string escape(string str)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(str), writer, null);
                    return writer.ToString();
                }
            }
        }

        // Converts a string to stream
        private Stream asStream(string str)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);

            return new MemoryStream(byteArray);
        }
    }
}