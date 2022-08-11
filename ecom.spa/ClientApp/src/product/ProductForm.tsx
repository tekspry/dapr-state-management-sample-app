import React, { useState } from "react";
import toBase64 from "../toBase64";
import { Product } from "../types/product";

type Args = {
    product: Product;
  submitted: (product: Product) => void;
};

const ProductForm = ({ product, submitted }: Args) => {
  const [productState, setProductState] = useState({ ...product });

  const onSubmit: React.MouseEventHandler<HTMLButtonElement> = async (e) => {
    e.preventDefault();
    submitted(productState);
  };

  const onFileSelected = async (
    e: React.ChangeEvent<HTMLInputElement>
  ): Promise<void> => {
    e.preventDefault();
    e.target.files &&
      e.target.files[0] &&
      setProductState({
        ...productState,        
        imageUrl: await e.target.files[0].name,
      });
  };

  return (
    <form className="mt-2">
      <div className="form-group">
        <label htmlFor="name">Product Name</label>
        <input
          type="text"
          className="form-control"
          placeholder="Product Name"
          value={productState.name}
          onChange={(e) =>
            setProductState({ ...productState, name: e.target.value })
          }
        />
      </div>
      <div className="form-group mt-2">
        <label htmlFor="seller">Seller</label>
        <input
          type="text"
          className="form-control"
          placeholder="Seller"
          value={productState.seller}
          onChange={(e) =>
            setProductState({ ...productState, seller: e.target.value })
          }
        />
      </div>
      <div className="form-group mt-2">
        <label htmlFor="description">Description</label>
        <textarea
          className="form-control"
          placeholder="Description"
          value={productState.description}
          onChange={(e) =>
            setProductState({ ...productState, description: e.target.value })
          }
        />
      </div>
      <div className="form-group mt-2">
        <label htmlFor="price">Price</label>
        <input
          type="number"
          className="form-control"
          placeholder="Price"
          value={productState.price}
          onChange={(e) =>
            setProductState({ ...productState, price: parseInt(e.target.value) })
          }
        />
      </div>
      <div className="form-group mt-2">
        <label htmlFor="image">Image</label>
        <input
          id="image"
          type="file"
          className="form-control"
          onChange={onFileSelected}
        />
      </div>
      
      <button
        className="btn btn-primary mt-2"
        disabled={!productState.name || !productState.price}
        onClick={onSubmit}
      >
        Submit
      </button>
    </form>
  );
};

export default ProductForm;
