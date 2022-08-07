import { useNavigate } from "react-router-dom";
import { useMutation, useQuery, useQueryClient } from "react-query";
import Config from "../config";
import axios, { AxiosError, AxiosResponse } from "axios";
import Problem from "../types/problem";
import { Product } from "../types/product";

const useFetchProducts = () => {
  return useQuery<Product[], AxiosError>("products", () =>
    axios.get(`${Config.baseApiUrl}/product`).then((resp) => resp.data)
  );
};

const useAddProduct = () => {
  const queryClient = useQueryClient();
  const nav = useNavigate();
  return useMutation<AxiosResponse, AxiosError<Problem>, Product>(
    (p) => axios.post(`${Config.baseApiUrl}/product`, p),
    {
      onSuccess: () => {
        queryClient.invalidateQueries("product");
        nav("/");
      },
    }
  );
};

export { useFetchProducts, useAddProduct};