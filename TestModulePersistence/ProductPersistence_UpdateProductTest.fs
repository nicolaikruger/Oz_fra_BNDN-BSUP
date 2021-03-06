﻿namespace RentIt.Test
  open Xunit
  open FsUnit.Xunit
  open RentIt.ProductTypes
  open RentIt.ProductPersistence
  open RentIt.ProductExceptions

  module TestUpdateProduct =
    [<Fact>]
    let ``Test update product name``() =
      let test = "TestUpdateProductName"
      try
        //Create test data
        let testProd = ref (Helper.createTestProduct test)
        let prev = (!testProd).name
        let update = "Updated name"
        //Change to updated name and test for change
        testProd := {!testProd with name = update}
        testProd := updateProduct !testProd
        (!testProd).name |> should equal update
        //Change back to prev and test for change
        testProd := {!testProd with name = prev}
        testProd := updateProduct !testProd
        (!testProd).name |> should equal prev
      finally
        //Clean up testdata
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product createDate``() =
      let test = "TestUpdateProductCreateDate"
      try
        //Create test data
        let testProd = ref (Helper.createTestProduct test)
        let prev = (!testProd).createDate
        let update = (System.DateTime.Parse "2013-01-01 10:00:00")
        //Change to updated name and test for change
        testProd := {!testProd with createDate = update}
        testProd := updateProduct !testProd
        (!testProd).createDate |> should equal update
        //Change back to prev and test for change
        testProd := {!testProd with createDate = prev}
        testProd := updateProduct !testProd
        (!testProd).createDate |> should equal prev
      finally
        //Clean up testdata
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product productType``() =
      let test = "TestUpdateProductProductType"
      try
        //Create test data
        let testProd = ref (Helper.createTestProduct test)
        let prev = (!testProd).productType
        let update = Helper.createTestType (test+"2")
        //Change to updated name and test for change
        testProd := {!testProd with productType = update}
        testProd := updateProduct !testProd
        (!testProd).productType |> should equal update
        //Change back to prev and test for change
        testProd := {!testProd with productType = prev}
        testProd := updateProduct !testProd
        (!testProd).productType |> should equal prev
      finally
        //Clean up testdata
        Helper.removeTestType (test+"2")
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product owner``() =
      let test = "TestUpdateProductOwner"
      try
        //Create test data
        let testProd = ref (Helper.createTestProduct test)
        let prev = (!testProd).owner
        let update = Helper.createTestUser (test+"2")
        //Change to updated name and test for change
        testProd := {!testProd with owner = update}
        testProd := updateProduct !testProd
        (!testProd).owner |> should equal update
        //Change back to prev and test for change
        testProd := {!testProd with owner = prev}
        testProd := updateProduct !testProd
        (!testProd).owner |> should equal prev
      finally
        //Clean up testdata
        Helper.removeTestUser (test+"2")
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product published``() =
      let test = "TestUpdateProductPublished"
      try
        //Create test data
        let testProd = ref (Helper.createTestProduct test)
        let prev = (!testProd).published
        let update = false
        //Change to updated name and test for change
        testProd := {!testProd with published = update}
        testProd := updateProduct !testProd
        (!testProd).published |> should equal update
        //Change back to prev and test for change
        testProd := {!testProd with published = prev}
        testProd := updateProduct !testProd
        (!testProd).published |> should equal prev
      finally
        //Clean up testdata
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product description``() =
      let test = "TestUpdateProductDescription"
      try
        //Create test data
        let testProd = ref (Helper.createTestProduct test)
        let prev = (!testProd).description
        let update = Some "New description"
        //Change to updated name and test for change
        testProd := {!testProd with description = update}
        testProd := updateProduct !testProd
        (!testProd).description |> should equal update
        //Change back to prev and test for change
        testProd := {!testProd with description = prev}
        testProd := updateProduct !testProd
        (!testProd).description |> should equal prev
      finally
        //Clean up testdata
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product rent price``() =
      let test = "TestUpdateProductRentPrice"
      try
        //Create test data
        let testProd = ref (Helper.createTestProduct test)
        let prev = (!testProd).rentPrice
        let update = Some 1000
        //Change to updated name and test for change
        testProd := {!testProd with rentPrice = update}
        testProd := updateProduct !testProd
        (!testProd).rentPrice |> should equal update
        //Change back to prev and test for change
        testProd := {!testProd with rentPrice = prev}
        testProd := updateProduct !testProd
        (!testProd).rentPrice |> should equal prev
      finally
        //Clean up testdata
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product buy price``() =
      let test = "TestUpdateProductDescription"
      try
        //Create test data
        let testProd = ref (Helper.createTestProduct test)
        let prev = (!testProd).buyPrice
        let update = Some 1000
        //Change to updated name and test for change
        testProd := {!testProd with buyPrice = update}
        testProd := updateProduct !testProd
        (!testProd).buyPrice |> should equal update
        //Change back to prev and test for change
        testProd := {!testProd with buyPrice = prev}
        testProd := updateProduct !testProd
        (!testProd).buyPrice |> should equal prev
      finally
        //Clean up testdata
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product with wrong type``() =
      let test = "TestUpdateProductWrongType"
      try
        let testProduct = ref (Helper.createTestProduct test)
        testProduct := {!testProduct with productType = "DoesNotExist"}
        (fun () -> (updateProduct !testProduct) |> ignore) |> should throw typeof<NoSuchProductType>
      finally
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product with wrong user``() =
      let test = "TestUpdateProductWrongUser"
      try
        let testProduct = ref (Helper.createTestProduct test)
        testProduct := {!testProduct with owner = "DoesNotExist"}
        (fun () -> (updateProduct !testProduct) |> ignore) |> should throw typeof<NoSuchUser>
      finally
        Helper.removeTestProduct test

    [<Fact>]
    let ``Test update product with wrong id``() =
      let test = "TestUpdateProductWrongId"
      try
        let testProduct = ref (Helper.createTestProduct test)
        testProduct := {!testProduct with id = 7238}
        (fun () -> (updateProduct !testProduct) |> ignore) |> should throw typeof<NoSuchProduct>
      finally
        Helper.removeTestProduct test