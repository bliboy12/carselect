import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { createBrowserRouter, RouterProvider } from 'react-router'
import RootLayout from './Layout/RootLayout.tsx'
import LoginPage from './pages/LoginPage.tsx'
import RegisterPage from './pages/RegisterPage.tsx'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import HomePage from './pages/HomePage.tsx'
import DetailListingPage from './pages/DetailListingPage.tsx'
import ProfilePage from './pages/ProfilePage.tsx'
import CreateListingPage from './pages/CreateListingPage.tsx'
import EditListingPage from './pages/EditListingPage.tsx'
import AdminPage from './pages/AdminPage.tsx'
import FavoritesPage from './pages/FavoritesPage.tsx'
import { AuthProvider } from "react-oidc-context";
import { authConfig } from './auth/authConfig.ts'
import CallbackPage from './pages/CallBackPage.tsx'
import ProtectedRoute from './components/ProtectedRoute.tsx'
import UserProvider from './context/UserContext.tsx'


const query = new QueryClient();

const router = createBrowserRouter([
  {
    element: <RootLayout />,
    children: [
      {
        element: <HomePage />,
        path: "/"
      },
      {
        element: <LoginPage />,
        path: "/login"
      },
      {
        element: <RegisterPage />,
        path: "/register"
      },
      {
        element: <DetailListingPage />,
        path: "/listings/:id"
      },
      {
        element: <ProtectedRoute><ProfilePage /></ProtectedRoute>,
        path: "/profile"
      },
      {
        element: <ProtectedRoute><CreateListingPage /></ProtectedRoute>,
        path: "listings/create"
      },
      {
        element: <ProtectedRoute><EditListingPage /></ProtectedRoute>,
        path: "listings/:id/edit"
      },
      {
        element: <ProtectedRoute adminOnly={true}><AdminPage /></ProtectedRoute>,
        path: "/adminpanel"
      },
      {
        element: <ProtectedRoute><FavoritesPage /></ProtectedRoute>,
        path: "/favorites"
      },
      {
        element: <CallbackPage />,
        path: "/callback"
      }
    ]
  }
])

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AuthProvider {...authConfig}>
      <UserProvider>
        <QueryClientProvider client={query}>
          <RouterProvider router={router} />
        </QueryClientProvider>
      </UserProvider>
    </AuthProvider>
  </StrictMode>,
)
