import { Card, Grid } from "@mui/material";
import { Group } from "../../../app/models/group";

interface Props {
  group: Group;
}

const GroupCard = ({ group }: Props) => {
  return (
    <Grid key={group.id} item xs={12} sm={6} md={4} lg={3}>
      <Card variant="outlined" className="room-card">
        <h1>{group.id}</h1>
        {group.members.map((user, i) => (
          <h2 key={i}>
            {user.firstName} {user.lastName}
          </h2>
        ))}
      </Card>
    </Grid>
  );
};

export default GroupCard;
