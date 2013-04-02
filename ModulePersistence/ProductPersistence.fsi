﻿namespace RentIt

module ProductPersistence =

  type Product = ProductTypes.Product
  
  exception NoSuchProduct
  exception NoSuchProductType
  exception NoSuchUser
  exception ProductNotPublished
  exception ProductAlreadyExists
  
  /// <summary>
  /// Creater in persistence layer
  /// </summary>
  /// <typeparam> Product </typeparam>
  /// <returns> Product </returns>
  /// <exception> ProductAlreadyExists </exception>
  /// <exception> NoSuchUser </exception>
  /// <exception> NoSuchProductType </exception>
  val createProduct : Product -> Product

  /// <summary>
  /// Update an existing product
  /// </summary>
  /// <typeparam> Product </typeparam>
  /// <returns> Product </returns>
  /// <exception> NoSuchProduct </exception>
  /// <exception> NoSuchUser </exception>
  /// <exception> NoSuchProductType </exception>
  val updateProduct : Product -> Product

  /// <summary>
  /// Get Product by its id
  /// </summary>
  /// <typeparam> Product id </typeparam>
  /// <returns> Product </returns>
  /// <exception> NoSuchProduct </exception>
  val getProductById : int -> Product

  /// <summary>
  /// Get a list of Products by Product name
  /// </summary>
  /// <typeparam> Product name </typeparam>
  /// <returns> Product list </returns>
  /// <exception> NoSuchProduct </exception>
  val getProductByName : string -> Product list

  /// <summary>
  /// Get a list of Products by Product type
  /// </summary>
  /// <typeparam> Product type </typeparam>
  /// <returns> Product list </returns>
  /// <exception> NoSuchProduct </exception>
  /// <exception> NoSuchProductType </exception>
  val getProductByType : string -> Product list

  /// <summary>
  /// Rate Product
  /// </summary>
  /// <typeparam> Product id </typeparam>
  /// <typeparam> Rating </typeparam>
  /// <returns> The product rated </returns>
  /// <exception> NoSuchProduct </exception>
  val rateProduct : int -> string -> int -> Product

  /// <summary>
  /// Change Published-flag on Product
  /// </summary>
  /// <typeparam> Product id </typeparam>
  /// <typeparam> Boolean </typeparam>
  /// <returns> The product changed </returns>
  /// <exception> NoSuchProduct </exception>
  val publishProduct : int -> bool -> Product

  /// <summary>
  /// Create a product type
  /// </summary>
  /// <typeparam> Name of the product  type <//typeparam>
  /// <returns> Bool depending on success </returns>
  val createProductType : string -> bool