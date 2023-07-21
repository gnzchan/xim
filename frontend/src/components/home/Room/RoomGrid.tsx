import { Grid } from "@mui/material";
import { Room } from "../../../app/models/room";
import RoomCard from "./RoomCard";

interface Props {
  rooms: Room[];
}

const RoomGrid = ({ rooms }: Props) => {
  // console.log(rooms);
  return (
    <Grid
      container
      className="flex-centered"
      sx={{ width: "100%", height: "100%" }}
    >
      {rooms.length === 0 ? (
        <h1>no rooms to display</h1>
      ) : (
        rooms.map((room: Room) => (
          <RoomCard
            key={room.roomId}
            roomId={room.roomId}
            roomName={room.roomName}
            hostUsername={room.hostUsername}
            capacity={room.capacity}
            roomCode={room.roomCode}
          />
        ))
      )}
    </Grid>
  );
};

export default RoomGrid;
