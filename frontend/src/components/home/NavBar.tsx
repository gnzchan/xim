import { Add, Logout, PersonAdd, RecentActors } from "@mui/icons-material";
import {
  BottomNavigation,
  BottomNavigationAction,
  Box,
  Grid,
} from "@mui/material";

import { useDispatch } from "react-redux";
import { Link } from "react-router-dom";
import { logout } from "../../app/store/auth/authSlice";

interface Props {
  activeTab: string;
  handleActiveTabChange: (
    event: React.SyntheticEvent,
    activatedTab: string
  ) => void;
}

const NavBar = ({ activeTab, handleActiveTabChange }: Props) => {
  const dispatch = useDispatch();
  return (
    <Box sx={{ width: "100%" }}>
      <Grid container>
        <Grid item xs={2} md={4} lg={1}></Grid>
        <Grid item xs={8} md={4} lg={10} className="flex-centered">
          <Link to="/home" className="link-reset">
            <BottomNavigation
              sx={{
                width: "100%",
              }}
              value={activeTab}
              onChange={handleActiveTabChange}
            >
              <BottomNavigationAction
                label="Join"
                value="join"
                icon={<PersonAdd />}
              />
              <BottomNavigationAction
                label="Create"
                value="create"
                icon={<Add />}
              />
              <BottomNavigationAction
                label="Rooms"
                value="rooms"
                icon={<RecentActors />}
              />
            </BottomNavigation>
          </Link>
        </Grid>
        <Grid item xs={2} md={4} lg={1} className="flex-centered">
          <BottomNavigationAction
            label="Logout"
            value="logout"
            onClick={() => {
              dispatch(logout());
            }}
            icon={<Logout />}
          />
        </Grid>
      </Grid>
    </Box>
  );
};

export default NavBar;
