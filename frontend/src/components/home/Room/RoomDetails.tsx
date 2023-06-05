import { Box, Button, Typography } from "@mui/material";
import { Form, Formik } from "formik";
import { useParams } from "react-router-dom";
import { useGetRoomQuery } from "../../../app/store/room/roomsApiSlice";
import InputField from "../../common/InputField";
import { useState } from "react";
import GroupsGrid from "../Groups/GroupsGrid";
import { selectCurrentUser } from "../../../app/store/auth/authSlice";
import { useSelector } from "react-redux";
import { User } from "../../../app/models/user";
import { createShuffleGroupsSchema } from "../../../app/schemas";

const RoomDetails = () => {
  const { roomId } = useParams<{ roomId: string }>();
  const { data: room } = useGetRoomQuery(roomId!);
  const currentUser: User = useSelector(selectCurrentUser);

  const [numOfGroups, setNumOfGroups] = useState(0);

  const ATTENDEES_COUNT = room?.attendees.length ?? 0;
  const shuffleGroupsSchema = createShuffleGroupsSchema(ATTENDEES_COUNT);

  return (
    <>
      {room && (
        <Box sx={{ border: "1px solid red", width: "80%", height: "100%" }}>
          <Box>
            <Box sx={{ marginBlock: "30px" }}>
              <Typography variant="h4">
                {room.roomCode} - {room.roomName}
              </Typography>
              <Typography variant="subtitle1">
                {room.hostUsername} ·{" "}
                <b>
                  {room.attendees.length}/{room.capacity}
                </b>
              </Typography>
            </Box>

            {room.attendees.map((attendee, i) => (
              <p key={i}>{attendee.username}</p>
            ))}
          </Box>
          <Box>
            {roomId && numOfGroups > 0 && (
              <GroupsGrid roomId={roomId} numOfGroups={numOfGroups} />
            )}
            {currentUser.username === room.hostUsername && (
              <Formik
                initialValues={{ numOfGroups: 1 }}
                validationSchema={shuffleGroupsSchema}
                onSubmit={async (values, actions) => {
                  setNumOfGroups(values.numOfGroups);
                }}
              >
                {(formik) => (
                  <Form style={{ width: "100%" }} noValidate>
                    <InputField
                      type="number"
                      inpProps={{
                        style: {
                          textAlign: "center",
                        },
                      }}
                      {...formik.getFieldProps("numOfGroups")}
                    />
                    <Button
                      type="submit"
                      variant="outlined"
                      color="primary"
                      fullWidth
                    >
                      Shuffle
                    </Button>
                  </Form>
                )}
              </Formik>
            )}
          </Box>
        </Box>
      )}
    </>
  );
};

export default RoomDetails;
