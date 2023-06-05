import { createBrowserRouter, Navigate, RouteObject } from "react-router-dom";
import Login from "../../components/auth/login/Login";
import Register from "../../components/auth/register/Register";
import Home from "../../components/home/Home";
import RoomDetails from "../../components/home/Room/RoomDetails";
import App from "../../layout/App";
import Auth from "../../layout/Auth";
import RequireAuth from "../../layout/RequireAuth";

export const routes: RouteObject[] = [
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "auth",
        element: <Auth />,
        children: [
          { path: "login", element: <Login /> },
          { path: "register", element: <Register /> },
        ],
      },
      {
        path: "home",
        element: <RequireAuth />,
        children: [
          { path: "", element: <Home /> },
          { path: "rooms/:roomId", element: <RoomDetails /> },
        ],
      },
      // {
      //   path: "rooms",
      //   element: <RequireAuth />,
      //   children: [{ path: ":roomId", element: <Room /> }],
      // },
      { path: "*", element: <Navigate replace to="/not-found" /> },
    ],
  },
];

export const router = createBrowserRouter(routes);
