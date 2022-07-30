import "./App.css";

import { BrowserRouter, Routes, Route } from "react-router-dom";
import HouseList from "../house/HouseList";
import HouseEdit from "../house/HouseEdit";
import HouseAdd from "../house/HouseAdd";
import HouseDetail from "../house/HouseDetail";
import Header from "./Header";
import ProductList from "../product/ProductList";
import ProductAdd from "../product/ProductAdd";

function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <Header subtitle="Sample DAPR application" noOfItems="0" />
        <Routes>
          <Route path="/" element={<ProductList />}></Route>
          <Route path="/product/add" element={<ProductAdd />}></Route>
          <Route path="/house/edit/:id" element={<HouseEdit />}></Route>
          <Route path="/house/:id" element={<HouseDetail />}></Route>
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
