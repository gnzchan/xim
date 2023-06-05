import { Button, Grid } from "@mui/material";
import { Box } from "@mui/system";
import { Form, Formik } from "formik";
import { Link, useNavigate } from "react-router-dom";
import { loginSchema } from "../../../app/schemas";
import InputField from "../../common/InputField";

import { useDispatch } from "react-redux";
import { setCredentials } from "../../../app/store/auth/authSlice";
import { useLoginMutation } from "../../../app/store/auth/authApiSlice";

const Login = () => {
  const [login, { isLoading }] = useLoginMutation();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  return isLoading ? (
    <h1>Loading</h1>
  ) : (
    <Grid item xs={12} md={6} className="flex-centered">
      <Box
        sx={{
          flexDirection: "column",
          width: "70%",
          maxWidth: "450px",
        }}
      >
        <Formik
          initialValues={{
            email: "",
            password: "",
          }}
          validationSchema={loginSchema}
          onSubmit={async (values, actions) => {
            actions.resetForm();
            try {
              const userData = await login({
                email: values.email,
                password: values.password,
              }).unwrap();
              dispatch(setCredentials({ ...userData }));
              navigate("/home");
            } catch (error) {
              console.log("error here");
              console.log(error);
            }
          }}
        >
          {(formik) => (
            <Form noValidate onSubmit={formik.handleSubmit}>
              <InputField
                type="email"
                label="Email"
                {...formik.getFieldProps("email")}
              />
              <InputField
                type="password"
                label="Password"
                {...formik.getFieldProps("password")}
              />
              <Button
                type="submit"
                variant="contained"
                fullWidth
                sx={{ marginBlock: 3 }}
              >
                Login
              </Button>
            </Form>
          )}
        </Formik>

        <Box alignSelf="flex-end">
          <p>
            Dont have an account? <Link to="/auth/register">Sign up</Link>
          </p>
        </Box>
      </Box>
    </Grid>
  );
};

export default Login;
