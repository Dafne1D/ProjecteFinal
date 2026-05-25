# DOCUMENTACIÓ DE PROJECTE
# APP “TaverEat”
## PER DAFNE CARRILLO REYES

<br><br><br><br><br><br><br><br>

### 2n DAM INS. BOSC DE LA COMA
### 19/05/2026

---

# Índex

1. Introducció  
2. Anàlisi de projecte  
   - Funcions segons tipus d’usuari  
3. Tecnologies utilitzades  
   - Backend  
   - Frontend  
   - Base de dades  
   - Control de versions  
4. Arquitectura de sistema  
5. Peticions API  
   - Categories  
   - Clients  
   - Autenticació  
   - Carret  
   - Productes  
6. Conclusions  
7. Bibliografia  

---

# Introducció

Aquest projecte consisteix en el desenvolupament d’una aplicació web inspirada en JustEat, però adaptada al bar de la meva mare: “La Taverneta”. El principi d’aquest projecte és modernitzar el nostre bar aprofitant que al nostre poble no arriben serveis com aquest.

L’aplicació permet consultar el menú, realitzar una comanda i fer el pagament de forma online.

A més a més de la part destinada als clients, també he desenvolupat un panell administratiu en el qual es podran visualitzar les comandes fetes.

Fer aquest projecte m’ha permès aplicar els coneixements relacionats amb el desenvolupament web, la gestió de bases de dades i la comunicació entre frontend i backend.

L’objectiu final del projecte és desenvolupar una aplicació web per al gestionament de comandes utilitzant tecnologies com ASP.NET Core i Next.js.

---

# Anàlisi de projecte

## Funcions segons tipus d’usuari

### Usuari
- Registre i inici de sessió dins la plataforma
- Consultar el menú
- Afegir i eliminar productes del carret
- Fer pagament online
- Consultar l’estat de la comanda

### Panell administratiu / cuiner
- Visualitzar les comandes
- Actualitzar l’estat d’aquestes

---

# Tecnologies utilitzades

Per al desenvolupament d’aquest projecte s’han fet servir diferents tecnologies, per tal d’assegurar l’escalabilitat del sistema i facilitar-ne el manteniment.

## Backend

Per al backend he utilitzat ASP.NET Core, el qual permet la creació d’una API segura per gestionar la lògica de l’aplicació, la comunicació amb la base de dades i l’autenticació d’usuaris mitjançant tokens.

També he aplicat una arquitectura basada en capes (endpoints, serveis i repositoris) per millorar l’organització del codi i facilitar-ne el manteniment.

## Frontend

Per al frontend he fet servir Next.js, que em permet desenvolupar una interfície basada en components.

Aquesta tecnologia facilita l’experiència d’usuari, especialment en aplicacions web amb moltes interaccions com és el cas d’un sistema de comandes.

- Vercel
- Docker

## Base de dades

Pel que fa a la base de dades he utilitzat SQL Server, permetent l’emmagatzematge i la gestió de forma estructurada de la informació relacionada amb usuaris, productes i comandes.

La comunicació entre l’aplicació i la base de dades es realitza a través del backend, assegurant la integritat i seguretat de les dades.

També he utilitzat:
- DBeaver

## Control de versions

Pel control de versions faig servir GitHub.

---

# Arquitectura de sistema

El funcionament general del sistema es basa en el següent flux:

1. L’usuari interactua amb el frontend (Next.js)
2. El frontend envia peticions a l’API del backend
3. El backend processa les peticions, aplica la lògica de negoci i accedeix a la base de dades
4. La resposta es retorna al frontend i es mostra a l’usuari

---

# Peticions API

# Categories

## GET ALL

```http
GET http://localhost:5000/categories
```

```json
[
    {
        "nom": "Begudes"
    },
    {
        "nom": "Entrepans"
    },
    {
        "nom": "Frankfourt"
    },
    {
        "nom": "Hamburgueses"
    },
    {
        "nom": "Pizzes"
    },
    {
        "nom": "Tapes"
    }
]
```

## GET ALL INFO BY CATEGORY

```http
GET http://localhost:5000/products/category/begudes/full
```

```json
[
    {
        "id": "c4a4d1d7-420b-49ae-af15-10d62240769e",
        "nom": "Nestea",
        "descripcio": "Llauna de 330 ml",
        "preu": 2.30,
        "imgUrl": "https://www.cellervalldoreix.com/wp-content/uploads/2019/07/Fashion-Bran-Logo-52.png"
    }
]
```

---

# Clients

## GET ALL

```http
GET http://localhost:5000/clients
```

```json
[
    {
        "id": "be4c4194-bb7d-4b9e-8fc9-ea496f4f6246",
        "nom": "Marcos",
        "email": "juan@email.com",
        "direccio": "BCN",
        "contrasenya": "1234"
    }
]
```

## GET BY ID

```http
GET http://localhost:5000/client/be4c4194-bb7d-4b9e-8fc9-ea496f4f6246
```

```json
{
    "id": "be4c4194-bb7d-4b9e-8fc9-ea496f4f6246",
    "nom": "Marcos",
    "email": "juan@email.com",
    "direccio": "BCN"
}
```

## POST CLIENT

```http
POST http://localhost:5000/clients
```

### Body

```json
{
  "nom": "Alvaro",
  "email": "alvaro@email.com",
  "direccio": "Barcelona",
  "contrasenya": "1234"
}
```

### Resultat

```json
{
    "id": "2bf2b439-9d82-4873-8d30-6a1e62208235",
    "nom": "Alvaro",
    "email": "alvaro@email.com",
    "direccio": "Barcelona"
}
```

## PUT CLIENT

```http
PUT http://localhost:5000/clients/2bf2b439-9d82-4873-8d30-6a1e62208235
```

### Body

```json
{
  "nom": "Alvaro2",
  "email": "alvaro@email.com",
  "direccio": "Barcelona",
  "contrasenya": "1234"
}
```

### Resultat

```json
{
    "id": "2bf2b439-9d82-4873-8d30-6a1e62208235",
    "nom": "Alvaro2",
    "email": "alvaro@email.com",
    "direccio": "Barcelona"
}
```

## DELETE CLIENT

```http
DELETE http://localhost:5000/clients/2bf2b439-9d82-4873-8d30-6a1e62208235
```

---

# Autenticació

## POST LOGIN

```http
POST http://localhost:5000/login
```

### Body

```json
{
    "email": "juan@email.com",
    "contrasenya": "1234"
}
```

### Resultat

```json
{
    "token": "JWT_TOKEN",
    "email": "juan@email.com"
}
```

---

# Carret

## GET CART

```http
GET http://localhost:5000/cart
Authorization: Bearer EL_TEU_TOKEN
```

```json
{
    "id": "233DA4AF-C7DD-4464-8F07-15C91546D181",
    "productes": [
        {
            "producteId": "825BCFAF-B9F5-4D3C-944B-22C8E05F0055",
            "nom": "Pizza Barbacoa",
            "preu": 8,
            "quantitat": 2
        }
    ],
    "total": 10.3
}
```

## PUT CART

```http
PUT http://localhost:5000/cart/item/update?producteId=233DA4AF-C7DD-4464-8F07-15C91546D181&quantitat=3
Authorization: Bearer YOUR_JWT_TOKEN
```

## DELETE ITEM

```http
DELETE http://localhost:5000/cart/item/825BCFAF-B9F5-4D3C-944B-22C8E05F0055
Authorization: Bearer YOUR_JWT_TOKEN
```

---

# Productes

## GET BY CATEGORY

```http
GET http://localhost:5000/products/category/hamburgueses
```

```json
[
    {
        "id": "69f35d6d-bbec-402f-97f3-41c9edbede9b",
        "nom": "Hamburguesa de Pollastre",
        "descripcio": "Hamburguesa de pollastre, formatge, ceba caramelitzada, tomaquet i enciam amb salsa brava.",
        "preu": 9.00,
        "categoria_nom": "Hamburgueses"
    }
]
```

---

# Conclusions

Al realitzar aquest projecte he pogut posar en pràctica diversos coneixements impartits durant el curs, especialment en el desenvolupament web, la gestió de bases de dades i la comunicació entre frontend i backend.

El fet de desenvolupar una aplicació pensada per a un negoci real m’ha permès entendre millor les necessitats que poden aparèixer en el meu entorn professional.

Durant el desenvolupament han anat sorgint diferents problemes tècnics, com ara l’autenticació, la gestió de dades i la integració entre Next.js i ASP.NET Core, els quals he anat solucionant a mesura que avançava en el projecte.

Tot i que el projecte actual compleix parcialment els objectius principals plantejats, pròximament integraré les funcionalitats no assolides o n’afegiré de noves.

En general, considero que aquest projecte ha estat una experiència molt útil a nivell tècnic, ja que m’ha permès aplicar els coneixements del curs en un projecte real relacionat amb el negoci familiar.

---

# Bibliografia

- Next.js  
https://nextjs.org/docs

- ASP.NET Core  
https://learn.microsoft.com/aspnet/core
