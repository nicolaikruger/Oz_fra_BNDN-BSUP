﻿namespace RentIt
module TestGetAllByType =

  open Xunit
  open FsUnit.Xunit

  type Product = ProductTypes.Product
  exception ArgumentException = RentIt.Product.ArgumentException
  exception NoSuchProduct = RentIt.Product.NoSuchProduct
  exception NoSuchProductType = RentIt.Product.NoSuchProductType

  [<Fact>]
  let ``get product types by name should work``() =
    let p = Product.getAllByType "Movie"
    p |> should not' (be null)
    p |> should be ofExactType<Product list>

  [<Fact>]
  let ``get product types by name should throw arg``() =
    (Product.getAllByType "" |> ignore) |> should throw typeof<ArgumentException>
  
  [<Fact>]
  let ``get product types by name should throw no such``() =
    (Product.getAllByType "This is very unlikely!" |> ignore) |> should throw typeof<NoSuchProductType>
   