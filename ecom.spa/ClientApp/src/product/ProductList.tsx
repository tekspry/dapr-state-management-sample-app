import { Product } from "../types/product";
import {useFetchProducts} from "../hooks/ProductHooks";
import { Link } from "react-router-dom";

const ProductList = () => {
  const { data } = useFetchProducts();    

    return (
        <div>
      <div className="row mb-2">
        <h5 className="themeFontColor text-center">
          Product List
        </h5>
      </div>
      <table className="table table-hover">
        <thead>
          <tr>
          <th>Product Image</th>
            <th>Product Name</th>
            <th>Product Price</th>
            <th>Seller Name</th>            
          </tr>
        </thead>
        <tbody>
            {data &&
                data.map((p: Product) => (
                    <tr key={p.productId}>
                        <td><img className="productImage" src={"http://localhost:3000/" + p.imageUrl}></img></td>
                        <td>{p.name}</td>
                        <td>{p.price}</td>
                        <td>{p.seller}</td>                       

                    </tr>
                
            ))}          
        </tbody>
      </table>   
      <Link className="btn btn-primary" to="/product/add">
        Add
      </Link>  
    </div>
    );
};

export default ProductList