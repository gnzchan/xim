import { Grid } from "@mui/material";
import { FieldHookConfig } from "formik";
import InputField from "../../common/InputField";

interface Props {
  type: string;
  label: string;
}

const RegisterInputField = (props: Props & FieldHookConfig<string>) => {
  return (
    <Grid item xs={12} md={6}>
      <InputField {...props} />
    </Grid>
  );
};

export default RegisterInputField;
