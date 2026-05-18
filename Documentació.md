# DOCUMENTACIÓ DE PROJECTE  
## APP “TaverEat”  
**Per Dafne Carrillo Reyes**

---

**2n DAM INS. BOSC DE LA COMA**  
**19/05/2026**

---

# Índex

- [Descripció del projecte](#descripció-del-projecte)
- [Objectius](#objectius)
- [Usuaris del sistema](#usuaris-del-sistema)
- [Funcions de client](#funcions-de-client)
- [Gestió de comandes](#gestió-de-comandes)
- [Arquitectura del sistema](#arquitectura-del-sistema)
  - [Frontend (Client)](#frontend-client)
  - [Backend](#backend)
  - [Model relacional](#model-relacional)
- [Introducció](#introducció)
- [Anàlisi del projecte](#anàlisi-del-projecte)
  - [Funcions segons tipus d’usuari](#funcions-segons-tipus-dusuari)
- [Tecnologies utilitzades](#tecnologies-utilitzades)
  - [Backend](#backend-1)
  - [Frontend](#frontend)
  - [Base de dades](#base-de-dades)
  - [Control de versions](#control-de-versions)
- [Arquitectura del sistema](#arquitectura-del-sistema-1)
- [Peticions](#peticions)
- [Bibliografia](#bibliografia)

---

# Introducció

Aquest projecte consisteix en el desenvolupament d’una app web inspirada en “JustEat”, però adaptada al bar de la meva mare: **La Taverneta**. El principi d’aquest projecte és modernitzar el nostre bar aprofitant que al nostre poble no arriben serveis com aquest.

L’aplicació permet consultar el menú, realitzar una comanda i fer el pagament de forma online.

A més a més de la part destinada als clients, també he desenvolupat un panell administratiu en el qual es podran visualitzar les comandes fetes.

Fer aquest projecte m’ha permès aplicar els coneixements relacionats amb el desenvolupament web, la gestió de bases de dades i la comunicació entre frontend i backend.

L’objectiu final del projecte és desenvolupar una app web per al gestionament de comandes utilitzant tecnologies com ASP.NET i Next.js.

---

# Anàlisi del projecte

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

Per al desenvolupament d’aquest projecte s’han fet servir diferents tecnologies per tal d’assegurar l’escalabilitat i facilitar-ne el manteniment.

## Backend

Per al backend he utilitzat ASP.NET Core, el qual permet la creació d’una API segura per gestionar la lògica de l’aplicació, la comunicació amb la base de dades i l’autentificació d’usuaris mitjançant tokens.

També he aplicat una arquitectura basada en capes (endpoints, serveis i repositoris) per millorar l’organització del codi i facilitar-ne el manteniment.

## Frontend

Per al frontend he fet servir Next.js, que em permet desenvolupar una interfície basada en components. Aquesta tecnologia facilita l’experiència d’usuari, especialment en aplicacions web amb moltes interaccions, com és el cas d’un sistema de comandes.

## Base de dades

Pel que fa a la base de dades, he utilitzat SQL Server, permetent l’emmagatzematge i la gestió estructurada de la informació relacionada amb usuaris, productes i comandes.

La comunicació entre l’aplicació i la base de dades es realitza a través del backend, assegurant la integritat i seguretat de les dades.

## Control de versions

Pel control de versions faig servir GitHub.

---

# Arquitectura del sistema

El funcionament general del sistema es basa en el següent flux:

1. L’usuari interactua amb el frontend (Next.js)
2. El frontend envia peticions a l’API del backend
3. El backend processa les peticions, aplica la lògica de negoci i accedeix a la base de dades
4. La resposta es retorna al frontend i es mostra a l’usuari

---

# Peticions

*(Apartat pendent de completar)*

---

# Bibliografia

## Next.js

- https://nextjs.org/docs

## ASP.NET

- https://learn.microsoft.com/es-es/aspnet/core/?view=aspnetcore-10.0