import "./App.css";

import { BrowserRouter, Routes, Route } from "react-router-dom";
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
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
