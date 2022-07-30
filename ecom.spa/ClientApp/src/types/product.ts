import {v4 as uuid} from 'uuid';

export type Product = {
        productId: string;
        name: string;
        price: number;
        seller: string;
        availableSince: string;
        description: string;
        imageUrl: string;
}