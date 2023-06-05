import { Grid } from "@mui/material";
import { Outlet } from "react-router-dom";

const Auth = () => {
  return (
    <Grid
      container
      sx={{
        height: "100%",
      }}
    >
      <Outlet />
      <Grid
        item
        xs
        sx={{
          backgroundColor: "green",
          height: "100%",
        }}
      ></Grid>
    </Grid>
  );
};

export default Auth;
