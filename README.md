[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/TWZD_Rru)
# web3-pg3-AutoSelect

Full-stack webapplicatie voor het zoeken, verkopen en kopen van auto's met rolgebaseerde toegang en betaalflow.

---

## 👤 Rollen

### User
- Registreren / inloggen  
- Auto’s zoeken, filteren en bekijken  
- Favorieten opslaan  
- Auto te koop aanbieden  
- Auto kopen + aankoopgeschiedenis  
- Export (PDF/CSV)

### Admin
- Gebruikers beheren (view/delete)  
- Listings en auto’s beheren  
- Overzicht van aankopen  

---

## ⚙️ Functionaliteit
- Server-side zoeken, filteren en paginatie  
- CRUD op cars en listings  
- Favorieten (alleen ingelogd)  
- Betaalflow (Stripe/Mollie) + webhook  
- Export (PDF/CSV)  
- UX states: loading, error, empty  

---

## 🛠️ Stack

### Frontend
- React + TypeScript (Vite)  
- React Router  
- React Hook Form / Formik  
- Axios  
- React Context (auth + favorites/cart)  

### Backend
- C# (.NET Web API)  
- REST + correcte statuscodes  
- Identity Server (OAuth / OIDC)  

### Storage
- Azure SQL → users, cars, orders  
- Cosmos DB → favorieten/logs  
- Blob Storage → afbeeldingen/exports  

---

## 🔗 Minimum requirements

### WEB3
- Entiteiten: User, Car, Listing, Order, Favorite (relaties)  
- CRUD per entiteit  
- Auth + rollen (user/admin)  
- Server-side search/filter/pagination  
- Payment + webhook  
- React Context  
- UX states  
- Export  

### PG3
- Azure SQL / Cosmos / Blob  
- Eigen API (frontend ↔ backend)  
- Externe API (Stripe/Mollie)  
- Identity Server  
- Deploy op Azure  
- OpenAPI + Postman  

---

## 📌 Opmerking
Scope kan nog beperkt worden indien nodig.
