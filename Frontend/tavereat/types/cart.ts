export type CartLinea = {
  producteId: string;
  nom: string;
  preu: number;
  quantitat: number;
};

export type CartData = {
  comandaId: string;
  productes: CartLinea[];
  total: number;
};