import { Box, Button, Typography } from "@mui/material";
import { Form, Formik } from "formik";
import { useParams } from "react-router-dom";
import { useGetRoomQuery } from "../../../app/store/room/roomsApiSlice";
import InputField from "../../common/InputField";
import { useEffect, useState } from "react";
import GroupsGrid from "../Groups/GroupsGrid";
import { selectCurrentUser } from "../../../app/store/auth/authSlice";
import { useSelector } from "react-redux";
import { User } from "../../../app/models/user";
import { createShuffleGroupsSchema } from "../../../app/schemas";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useLazyGetGroupsQuery } from "../../../app/store/group/groupsApiSlice";
import { Group } from "../../../app/models/group";

const RoomDetails = () => {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [trigger] = useLazyGetGroupsQuery();
  const currentUser: User = useSelector(selectCurrentUser);
  const { roomId } = useParams<{ roomId: string }>();
  const { data: room, refetch } = useGetRoomQuery(roomId!);

  const [groups, setGroups] = useState<Group[] | null>(null);
  const [numOfGroups, setNumOfGroups] = useState<number>(1);

  const ATTENDEES_COUNT = room?.attendees.length ?? 0;
  const shuffleGroupsSchema = createShuffleGroupsSchema(ATTENDEES_COUNT);

  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/hubs/room")
      .withAutomaticReconnect()
      .build();

    setConnection(connection);

    connection.on("SendMessage", (message) => {
      refetch();
    });
    connection.on("UpdateGroups", (group) => {
      refetch();
      setGroups(group);
    });

    const joinRoom = async () => {
      try {
        await connection.start();
        console.log("SignalR connection started.");

        await connection.invoke("JoinRoom", {
          roomId: roomId,
          appUser: currentUser,
        });
      } catch (error) {
        console.error("Error starting SignalR connection:", error);
      }
    };

    joinRoom();

    setGroups(room!.groups);
  }, []);

  const updateGroups = async (num: number) => {
    try {
      const result = await trigger({
        roomId,
        numOfGroups: num,
      });

      setGroups(result?.data ?? null);

      await connection?.invoke("ReceiveGroups", {
        roomId: roomId,
        groups: result.data,
      });
    } catch (error) {
      console.error("Error invoking ReceiveGroups:", error);
    }
  };

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
            {roomId && numOfGroups > 0 && <GroupsGrid groups={groups} />}
            {currentUser.username === room.hostUsername && (
              <Formik
                initialValues={{ numOfGroups: numOfGroups }}
                validationSchema={shuffleGroupsSchema}
                onSubmit={(values) => {
                  updateGroups(values.numOfGroups);
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
