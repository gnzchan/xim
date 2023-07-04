import { Box, Button, Typography } from "@mui/material";
import { Formik, Form } from "formik";
import { joinRoomSchema } from "../../../app/schemas";
import { useJoinRoomMutation } from "../../../app/store/room/roomsApiSlice";
import { useNavigate } from "react-router-dom";
import InputField from "../../common/InputField";

const JoinRoom = () => {
  const [joinRoom] = useJoinRoomMutation();
  const navigate = useNavigate();

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
      <Typography variant="h4">Enter room code</Typography>
      <Formik
        initialValues={{ roomCode: "" }}
        validationSchema={joinRoomSchema}
        onSubmit={async (values, actions) => {
          actions.resetForm();
          try {
            await joinRoom(values.roomCode).unwrap();
          } catch (error) {
            console.log(error);
          }
        }}
      >
        {(formik) => (
          <Form style={{ width: "100%" }} noValidate>
            <InputField
              type="text"
              inpProps={{
                maxLength: 6,
                style: { textAlign: "center", textTransform: "uppercase" },
              }}
              {...formik.getFieldProps("roomCode")}
            />
            <Button type="submit" variant="outlined" color="primary" fullWidth>
              Join
            </Button>
          </Form>
        )}
      </Formik>
    </Box>
  );
};

export default JoinRoom;
