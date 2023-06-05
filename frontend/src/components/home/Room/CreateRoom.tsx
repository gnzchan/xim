import { Box, Button, Typography } from "@mui/material";
import { Form, Formik } from "formik";
import InputField from "../../common/InputField";

import { useCreateRoomMutation } from "../../../app/store/room/roomsApiSlice";
import { createRoomSchema } from "../../../app/schemas";

const CreateRoom = () => {
  const [createRoom] = useCreateRoomMutation();

  return (
    <Box
      className="flex-centered"
      sx={{
        height: "100%",
        minWidth: "300px",
        maxWidth: "550px",
        width: "70%",
        flexDirection: "column",
      }}
    >
      <Typography variant="h4">Create room</Typography>
      <Formik
        initialValues={{
          roomName: "",
          capacity: 0,
        }}
        validationSchema={createRoomSchema}
        onSubmit={async (values, actions) => {
          actions.resetForm();
          try {
            await createRoom({ ...values }).unwrap();
          } catch (error) {
            console.log(error);
          }
        }}
      >
        {(formik) => (
          <Form style={{ width: "100%" }} noValidate>
            <InputField
              type="text"
              label="Room Name"
              inpProps={{ style: { textTransform: "uppercase" } }}
              {...formik.getFieldProps("roomName")}
            />
            <InputField
              type="number"
              label="Capacity"
              inpProps={{ min: 1, max: 30 }}
              {...formik.getFieldProps("capacity")}
            />
            <Button
              type="submit"
              variant="contained"
              fullWidth
              sx={{ marginBlock: 3 }}
            >
              Create
            </Button>
          </Form>
        )}
      </Formik>
    </Box>
  );
};

export default CreateRoom;
