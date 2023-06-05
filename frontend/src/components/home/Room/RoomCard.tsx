import { Card, Grid } from "@mui/material";
import { Link } from "react-router-dom";

interface Props {
  roomId: string;
  roomName: string;
  hostUsername: string;
  capacity: number;
  roomCode: string;
}

const RoomCard = ({
  roomId,
  roomName,
  hostUsername,
  capacity,
  roomCode,
}: Props) => {
  return (
    <Grid key={roomId} item xs={12} sm={6} md={4} lg={3}>
      <Link to={`rooms/${roomId}`}>
        <Card variant="outlined" className="room-card">
          <h1>{roomName}</h1>
          <h3>{hostUsername}</h3>
          <p>{capacity}</p>
          <p>{roomCode}</p>
        </Card>
      </Link>
    </Grid>
  );
};

export default RoomCard;
