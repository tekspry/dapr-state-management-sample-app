import { useNavigate } from "react-router-dom";
import { useMutation, useQuery, useQueryClient } from "react-query";
import Config from "../config";
import axios, { AxiosError, AxiosResponse } from "axios";
import Problem from "../types/problem";
import { useEffect, useState } from "react";
import { Product } from "../types/product";

// const useFetchHouses = (): House[] => {
//   const [allHouses, setAllHouses] = useState<House[]>([]);

//   useEffect(() => {
//     const fetchHouses = async () => {
//       const rsp = await fetch(`${Config.baseApiUrl}/houses`);
//       const houses = await rsp.json();
//       setAllHouses(houses);
//     };
//     fetchHouses();
//   }, []);

//   return allHouses;
// };

const useFetchProducts = () => {
  return useQuery<Product[], AxiosError>("products", () =>
    axios.get(`${Config.baseApiUrl}/product`).then((resp) => resp.data)
  );
};

const useAddProduct = () => {
  const queryClient = useQueryClient();
  const nav = useNavigate();
  return useMutation<AxiosResponse, AxiosError<Problem>, Product>(
    (h) => axios.post(`${Config.baseApiUrl}/product`, h),
    {
      onSuccess: () => {debugger
        queryClient.invalidateQueries("product");
        nav("/");
      },
    }
  );
};

export { useFetchProducts, useAddProduct};