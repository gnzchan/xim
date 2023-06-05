import { Box, Button, Grid } from "@mui/material";
import { Formik } from "formik";
import { Link } from "react-router-dom";
import { registerSchema } from "../../../app/schemas";

import RegisterInputField from "./RegisterInputField";
import { useDispatch } from "react-redux";
import { useRegisterMutation } from "../../../app/store/auth/authApiSlice";
import { useNavigate } from "react-router-dom";
import { setCredentials } from "../../../app/store/auth/authSlice";

const Register = () => {
  const [register] = useRegisterMutation();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  return (
    <Grid item xs={12} md={8} className="flex-centered">
      <Box
        sx={{
          flexDirection: "column",
          width: "70%",
        }}
      >
        <Formik
          initialValues={{
            firstName: "",
            lastName: "",
            username: "",
            email: "",
            password: "",
            confirmPassword: "",
          }}
          validationSchema={registerSchema}
          onSubmit={async (values, actions) => {
            actions.resetForm();
            try {
              const userData = await register(values).unwrap();
              dispatch(setCredentials({ ...userData }));
              navigate("/home");
            } catch (error) {
              console.log(error);
            }
          }}
        >
          {(formik) => (
            <Grid
              container
              component="form"
              onSubmit={formik.handleSubmit}
              spacing={2}
            >
              <RegisterInputField
                type="text"
                label="First Name"
                {...formik.getFieldProps("firstName")}
              />
              <RegisterInputField
                type="text"
                label="Last Name"
                {...formik.getFieldProps("lastName")}
              />
              <RegisterInputField
                type="text"
                label="Username"
                {...formik.getFieldProps("username")}
              />
              <RegisterInputField
                type="email"
                label="Email"
                {...formik.getFieldProps("email")}
              />
              <RegisterInputField
                type="password"
                label="Password"
                {...formik.getFieldProps("password")}
              />
              <RegisterInputField
                type="password"
                label="Confirm Password"
                {...formik.getFieldProps("confirmPassword")}
              />
              <Grid item xs={12}>
                <Button
                  type="submit"
                  variant="contained"
                  fullWidth
                  sx={{ marginBlock: 3 }}
                >
                  Register
                </Button>
              </Grid>
            </Grid>
          )}
        </Formik>
        <Box alignSelf="flex-end">
          <p>
            Already have an account? <Link to="/auth/login">Log in</Link>
          </p>
        </Box>
      </Box>
    </Grid>
  );
};

export default Register;
