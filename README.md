# 🚗 CarSelect

CarSelect is a full-stack car marketplace where users can browse, list and buy second-hand cars. Built as a school project for the Applied Computer Science program at HOGENT.

---

## ✨ Features

### 🔧 As an admin
- Manage all listings and users from a dedicated dashboard
- Promote or demote users to admin role
- Export all listings as a PDF report

### 🧑‍💼 As a seller
- Create, edit and delete your own car listings with image upload
- View and manage your active listings on your profile page

### 🛒 As a buyer
- Browse and filter car listings with server-side pagination
- Save listings to favourites
- Buy a car via Stripe payment flow
- Leave reviews for sellers

---

## 🛠️ Tech Stack

### Frontend
- React + TypeScript + Vite
- Tailwind CSS
- TanStack Query + Axios
- React Router + React Hook Form
- react-oidc-context (Duende IdentityServer)
- Stripe.js

### Backend — .NET 10

| Service | Responsibility |
|---|---|
| `CarSelect.API` | Listings, cars, users, favorites, payments, reviews aggregator |
| `CarSelect.Reviews.API` | Reviews microservice |
| `CarSelect.Identity` | Authentication via Duende IdentityServer |

### ☁️ Cloud (Azure)

| Service | Purpose |
|---|---|
| Azure App Service | Hosting backend services |
| Azure Blob Storage | Car images + static frontend hosting |
| Azure SQL | Users, favorites, reviews, transactions |
| Azure Cosmos DB | Listings, cars, car images |
| GitHub Actions | CI/CD pipeline |

---

## 🏗️ Architecture decisions

**Layered architecture**
Each backend project follows a strict layered architecture (API / Service / Repository / Domain). Contracts never appear in the service layer and repositories accept and return domain models.

**Cosmos DB for listings**
Listing data is document-heavy with nested car info and images, making NoSQL a natural fit. Users, favorites, reviews and transactions use Azure SQL for relational integrity.

**IdentityServer**
Authentication is handled entirely by Duende IdentityServer with ASP.NET Core Identity. A custom `ProfileService` adds role and name claims to the JWT token so the frontend can drive role-based UI.

**Reviews microservice**
Reviews run as a separate ASP.NET Core service with its own `ReviewsDbContext`. The main API acts as an aggregator — enriching review data with user information before returning it to the frontend.

**Service-to-service auth**
The main API authenticates against the Reviews microservice using OAuth2 client credentials, requesting a token from IdentityServer before each call.

**React Context**
`UserContext` provides a typed abstraction over the OIDC library exposing domain-friendly properties like `userId`, `isAdmin` and `token`. `FavoritesContext` tracks the favourites count globally, updating the navbar badge in real time.

**Stripe payments**
Payment intents are created server-side with listing and buyer metadata. A webhook endpoint handles `payment_intent.succeeded` — saving the transaction to SQL and marking the listing as sold.
