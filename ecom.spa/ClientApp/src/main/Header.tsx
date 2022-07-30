import { useNavigate } from "react-router-dom";
import logo from "./Teckspry-logo.png";
import cart from "./cart.png";
import {useFetchNoOfItems} from "../hooks/CartHooks";

type Args = {
  subtitle: string;
  noOfItems: string;
};

const Header = ({ subtitle }: Args) => {
    //const { data } = useFetchNoOfItems(); 
  const nav = useNavigate();
  return (
    <header className="row mb-4">
      <div className="col-4 mt-1 mb-1">
        <img src={logo} className="logo" alt="logo" onClick={() => nav("/")} />
      </div>
      <div className="col-5 mt-5 subtitle">{subtitle}</div>
      <div className="col-1 mt-1">        
      <img src={cart} className="cart" alt="cart" onClick={() => nav("/")} />
      </div>
    </header>
  );
};

export default Header;
