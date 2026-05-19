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

Aquest projecte consisteix en el desenvolupament d’una aplicació web inspirada en :contentReference[oaicite:0]{index=0}, però adaptada al bar de la meva mare: “La Taverneta”. El principi d’aquest projecte és modernitzar el nostre bar aprofitant que al nostre poble no arriben serveis com aquest.

L’aplicació permet consultar el menú, realitzar una comanda i fer el pagament de forma online.

A més a més de la part destinada als clients, també he desenvolupat un panell administratiu en el qual es podran visualitzar les comandes fetes.

Fer aquest projecte m’ha permès aplicar els coneixements relacionats amb el desenvolupament web, la gestió de bases de dades i la comunicació entre frontend i backend.

L’objectiu final del projecte és desenvolupar una aplicació web per al gestionament de comandes utilitzant tecnologies com :contentReference[oaicite:1]{index=1} i :contentReference[oaicite:2]{index=2}.

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

Per al backend he utilitzat :contentReference[oaicite:3]{index=3}, el qual permet la creació d’una API segura per gestionar la lògica de l’aplicació, la comunicació amb la base de dades i l’autenticació d’usuaris mitjançant tokens.

També he aplicat una arquitectura basada en capes (endpoints, serveis i repositoris) per millorar l’organització del codi i facilitar-ne el manteniment.

## Frontend

Per al frontend he fet servir :contentReference[oaicite:4]{index=4}, que em permet desenvolupar una interfície basada en components.

Aquesta tecnologia facilita l’experiència d’usuari, especialment en aplicacions web amb moltes interaccions com és el cas d’un sistema de comandes.

- :contentReference[oaicite:5]{index=5}
- Docker

## Base de dades

Pel que fa a la base de dades he utilitzat :contentReference[oaicite:6]{index=6}, permetent l’emmagatzematge i la gestió de forma estructurada de la informació relacionada amb usuaris, productes i comandes.

La comunicació entre l’aplicació i la base de dades es realitza a través del backend, assegurant la integritat i seguretat de les dades.

També he utilitzat:
- DBeaver

## Control de versions

Pel control de versions faig servir :contentReference[oaicite:7]{index=7}.

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
