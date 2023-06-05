import { InputBaseComponentProps, TextField } from "@mui/material";
import { useField, FieldHookConfig } from "formik";

interface Props {
  label?: string;
  type: string;
  inpProps?: InputBaseComponentProps | undefined;
}

const InputField = (props: Props & FieldHookConfig<string>) => {
  const [field, meta] = useField(props);
  return (
    <TextField
      {...field}
      label={props.label}
      type={props.type}
      inputProps={props.inpProps}
      helperText={meta.touched && meta.error}
      error={meta.touched && Boolean(meta.error)}
      variant="outlined"
      margin="normal"
      fullWidth
    />
  );
};

export default InputField;
