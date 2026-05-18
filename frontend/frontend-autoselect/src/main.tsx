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
        element: <ProfilePage />,
        path: "/profile"
      },
      {
        element: <CreateListingPage />,
        path: "listings/create"
      },
      {
        element: <EditListingPage />,
        path: "listings/:id/edit"
      },
      {
        element: <AdminPage />,
        path: "/adminpanel"
      },
      {
        element: <FavoritesPage />,
        path: "/favorites"
      }
    ]
  }
])

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryClientProvider client={query}>
      <RouterProvider router={router} />
    </QueryClientProvider>
  </StrictMode>,
)
