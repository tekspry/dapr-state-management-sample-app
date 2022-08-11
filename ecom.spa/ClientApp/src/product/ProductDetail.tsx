import { Link, useParams } from "react-router-dom";
import { useDeleteProduct, useFetchProduct } from "../hooks/ProductHooks";
import ApiStatus from "../apiStatus";
import { currencyFormatter } from "../config";
import defaultImage from "../main/logo.png";


const ProductDetail = () => {
  const { id } = useParams();
  if (!id) throw Error("Product id not found");

  const { data, status, isSuccess } = useFetchProduct(id);

  const deleteProductMutation = useDeleteProduct();

  if (!isSuccess) return <ApiStatus status={status} />;

  if (!data) return <div>Product not found.</div>;

  return (
    <div className="row">
      <div className="col-6">
        <div className="row">
          <img
            className="img-fluid"
            src={data.imageUrl ? data.imageUrl : defaultImage}
            alt="Product pic"
          />
        </div>      
      </div>
      <div className="col-6">
        <div className="row mt-2">
          <h5 className="col-12">{data.name}</h5>
        </div>
        <div className="row">
          <h3 className="col-12">{data.seller}</h3>
        </div>
        <div className="row">
          <h2 className="themeFontColor col-12">
            {currencyFormatter.format(data.price)}
          </h2>
        </div>
        <div className="row">
          <div className="col-12 mt-3">{data.description}</div>
        </div>        
      </div>
    </div>
  );
};

export default ProductDetail;
