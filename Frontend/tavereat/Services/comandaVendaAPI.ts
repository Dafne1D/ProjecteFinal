import { authFetch } from "./authFetch";

const API_URL = "http://localhost:5000";

export type CartLinea = {
    producteId: string,
    nom: string,
    preu: number,
    quantitat: number
};

export type Cart = {
    comandaId: string,
    productes: CartLinea[],
    total: number
};

// Get Carrito

export const getCart = async (): Promise<Cart> => {
    const res = await authFetch(`${API_URL}/cart`);

    if(!res.ok){
        throw new Error("Error al carregar el carrito")
    }
    return res.json();
};

// Afegir producte

export const addToCart = async (producteId: string) => {
    const res = await authFetch(`${API_URL}/cart/add`, {
        method: "POST",
        body: JSON.stringify({
            producteId,
        }),
    });
    
    if(!res.ok){
        throw new Error("Error afegint producte")
    }

    return res.json();
}


// Eliminar producte

export const deleteFromCart = async (producteId: string) => {
    const res = await authFetch(`${API_URL}/cart/remove/${producteId}`, {
        method: "DELETE",
    });

    if(!res.ok){
        throw new Error("Error eliminar producte del carrito")
    }

    return res.json();
}