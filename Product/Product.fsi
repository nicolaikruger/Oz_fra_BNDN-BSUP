namespace RentIt
open ProductTypes
open ProductExceptions

module Product =

  /// <summary>
  /// Creater
  ///</summary>
  /// <typeparam> UserId </typeparam>
  /// <typeparam> Name </typeparam>
  /// <typeparam> ProductType </typeparam>
  /// <typeparam> Description (optional) </typeparam>
  /// <typeparam> BuyPrice (optional) </typeparam>
  /// <typeparam> RentPrice (optional) </typeparam>
  /// <returns> Product </returns>
  /// <exception> RentIt.Product.NoSuchUser </exception>
  /// <exception> RentIt.Product.ArgumentException </exception>
  val make : string -> string -> string -> string option -> int option -> int option -> Product 
    
  /// <summary>
  /// Get prodcut by product id
  /// </summary>
  /// <typeparam> Id </typeparam>
  /// <returns> Product </returns>
  /// <exception> RentIt.Product.NoSuchProduct </exception>
  /// <exception> RentIt.Product.ArgumentException </exception>
  val getProductById : int -> Product 

  /// <summary>
  /// Persist a new product, making the product available for publish
  /// </summary>
  // <typeparam> Product </typeparam>
  /// <exception> RentIt.Product.ProductAlreadyExists </exception>
  /// <exception> RentIt.Product.NoSuchProductType </exception>
  /// <exception> RentIt.Product.NoSuchUser </exception>
  /// <exception> RentIt.Product.ArgumentException </exception>
  val persist : Product -> Product

  /// <summary>
  /// Get products by product name
  /// </summary>
  // <typeparam> Product name </typeparam>
  /// <returns> List of products </returns>
  /// <exception> RentIt.Product.NoSuchProduct </exception>
  /// <exception> RentIt.Product.ArgumentException </exception>
  val getProductByName : string -> Product list 

  /// <summary>
  /// Update existing product
  /// </summary>
  // <typeparam> Product name </typeparam>
  /// <exception> RentIt.Product.NoSuchProduct </exception>
  /// <exception> RentIt.Product.ArgumentException </exception>
  val update : Product -> Product

  /// <summary>
  /// Rate Product
  ///</summary>
  /// <typeparam> Product id </typeparam>
  /// <typeparam> Rating </typeparam>
  /// <exception> NoSuchProduct </exception>
  val rateProduct : int -> string -> int -> Product

 /// <summary>
  /// Change Published-flag on Product
  ///</summary>
  /// <typeparam> Product id </typeparam>
  /// <typeparam> Boolean </typeparam>
  /// <exception> NoSuchProduct </exception>
  val publishProduct : int -> bool -> Product

  /// <summary>
  /// Get a list of product types 
  /// </summary>
  /// <returns> String list of product types </returns>
  val getListOfProductTypes : unit -> string[]

  /// <summary>
  /// Returns all MIME types supported for a given product type
  ///</summary>
  /// <typeparam> product type </typeparam>
  val getMimesForProductType : string -> string list

  /// <summary>
  /// Persist a new media
  ///</summary>
  val persistMedia : uint32 -> string -> System.IO.Stream -> unit

  /// <summary>
  /// Persist a new media thumbnail
  ///</summary>
  val persistMediaThumbnail : uint32 -> string -> System.IO.Stream -> unit

  /// <summary>
  /// Gets a stream to the requeste media and the media MIME type
  ///</summary>
  /// <exception> MediaNotFound </exception>
  val getMedia : uint32 -> System.IO.FileStream*string

  /// <summary>
  /// Gets a stream to the requeste media thumbnail and the media MIME type
  ///</summary>
  /// <exception> MediaNotFound </exception>
  val getMediaThumbnail : uint32 -> System.IO.FileStream*string

  val searchProducts : string -> Product list

  val getAllProducts : PublishedStatus -> Product List

  val getAllProductsByUser : string -> PublishedStatus -> Product List

  val getAllProductsByType : string -> PublishedStatus -> Product List

  val getAllProductsByUserAndTitle : string -> string -> PublishedStatus -> Product List

  /// <summary>
  /// Checks if a media file is avalible for the given product
  ///</summary>
  val hasMedia : uint32 -> bool

  /// <summary>
  /// Checks if a thumbnail file is avalible for the given product
  ///</summary>
  val hasThumbnail : uint32 -> bool