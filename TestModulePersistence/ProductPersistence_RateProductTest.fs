﻿namespace RentIt.Test.ModulePersistence.ProductPersistence
  open Xunit
  open FsUnit.Xunit
  open RentIt.ProductTypes
  open RentIt.ProductPersistence

  module TestRateProduct =
    [<Fact>]
    let ``Test rate product``() =
      let test = "TestRateProduct"
      //Create test data
      let testProd = ref (Helper.createTestProduct test)
      //Create three test users
      let testUser1 = Helper.createTestUser (test+"2")
      let testUser2 = Helper.createTestUser (test+"3")
      let testUser3 = Helper.createTestUser (test+"4")
      //Rate for them
      testProd := rateProduct (!testProd).id testUser1 4
      testProd := rateProduct (!testProd).id testUser2 -2
      testProd := rateProduct (!testProd).id testUser3 1
      //Test that number of votes and rating are correct
      (!testProd).rating.Value.votes |> should equal 3
      (!testProd).rating.Value.rating |> should equal 1
      //Change one of the users ratings
      testProd := rateProduct (!testProd).id testUser2 1
      //Test that number of votes and rating are correct
      (!testProd).rating.Value.votes |> should equal 3
      (!testProd).rating.Value.rating |> should equal 2
      //Clean up
      Helper.removeTestUser (test+"2")
      Helper.removeTestUser (test+"3")
      Helper.removeTestUser (test+"4")
      Helper.removeTestProduct test