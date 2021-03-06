﻿namespace RentIt.Persistence
    ///Containing all the types needed by persistence API.
    ///Processor field is the function to transform data to a string used by query.
    module Types =
        ///A generic type defining a field anywhere. SELECT Field FROM .. or .. WHERE Field = ..
        type Field = {
            objectName : string;
            field : string; 
            processor : Field -> string;
        }
        ///Defining a filter in a query. In WHERE clause
        type Filter = {
            field : Field;
            value : string;
            processor : Filter -> string;
        }
        // Group of filters
        type FilterGroup = {
          filters : Filter List;
          joiner : string Option;
          processor : FilterGroup -> string
        }
        ///Defining a field to be read by a select query. SELECT ReadField FROM ..
        type ReadField = {
            field : Field;
            processor : ReadField -> string;
        }
        ///Defining a fields value from result of select query.
        ///TODO Not used. Don't know whether or not it should be
        type DataOut = {
            field : Field;
            value : string;
        }
        ///Defining the value of a field in the query. UPDATE .. SET DataIn ..
        type DataIn = {
            field : Field;
            value : string;
            processor : DataIn -> string;
        }
        ///Defining a join between two tables in database. .. JOIN ObjectJoin ..
        type ObjectJoin = {
            fieldFrom : Field;
            fieldTo : Field;
            processor : ObjectJoin -> string;
        }
        ///Defining a create query, and the information needed
        type Create = {
            objectName : string;
            data : DataIn List;
        }
        ///Defining a read query, and the information needed
        type Read = {
            fields : ReadField List;
            baseObjectName : string;
            joins : ObjectJoin List;
            filters : FilterGroup List;
            processor : Read -> string
        }
        ///Defining an update query, and the information needed
        type Update = {
            objectName : string;
            filters : FilterGroup List;
            data : DataIn List;
        }
        ///Defining a delete query, and the information needed
        type Delete = {
            objectName : string;
            filters : FilterGroup List;
        }
        ///Defining a transaction, with a sequence of queries
        type Transaction = {
            queries : string List;
        }