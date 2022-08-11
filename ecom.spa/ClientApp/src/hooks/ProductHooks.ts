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

const useFetchProduct = (id: string) => {
  return useQuery<Product, AxiosError>(["products", id], () =>
    axios.get(`${Config.baseApiUrl}/product/${id}`).then((resp) => resp.data)
  );
};

const useDeleteProduct = () => {
  const queryClient = useQueryClient();
  const nav = useNavigate();
  return useMutation<AxiosResponse, AxiosError, Product>(
    (p) => axios.delete(`${Config.baseApiUrl}/products/${p.productId}`),
    {
      onSuccess: () => {
        queryClient.invalidateQueries("products");
        nav("/");
      },
    }
  );
};

export { useFetchProducts, useAddProduct, useFetchProduct, useDeleteProduct};