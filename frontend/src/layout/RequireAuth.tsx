import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../app/store/auth/authSlice";
import { Box } from "@mui/material";
import NavBar from "../components/home/NavBar";
import { setActiveTab, selectActiveTab } from "../app/store/common/commonSlice";
import { useDispatch } from "react-redux";
import TopBar from "../components/home/TopBar";

const RequireAuth = () => {
  const dispatch = useDispatch();
  const token = useSelector(selectCurrentToken);
  const activeTab = useSelector(selectActiveTab);
  const location = useLocation();

  const handleActiveTabChange = (
    event: React.SyntheticEvent,
    activatedTab: string
  ) => {
    dispatch(setActiveTab(activatedTab));
  };

  return token ? (
    <Box
      className="flex-centered"
      sx={{
        height: "100%",
        width: "100%",
        flexDirection: "column",
      }}
    >
      <TopBar />

      <Box className="flex-centered" sx={{ height: "100%", width: "100%" }}>
        <Outlet />
      </Box>

      <NavBar
        activeTab={activeTab}
        handleActiveTabChange={handleActiveTabChange}
      />
    </Box>
  ) : (
    <Navigate to="/auth/login" state={{ from: location }} replace />
  );
};

export default RequireAuth;
