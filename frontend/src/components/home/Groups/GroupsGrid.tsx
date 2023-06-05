import { Grid } from "@mui/material";
import { Group } from "../../../app/models/group";
import { useGetGroupsQuery } from "../../../app/store/group/groupsApiSlice";
import GroupCard from "./GroupCard";

interface Props {
  roomId: string;
  numOfGroups: number;
}

const GroupsGrid = ({ roomId, numOfGroups }: Props) => {
  const { data: groups } = useGetGroupsQuery({
    roomId,
    numOfGroups,
  });

  return (
    <div>
      <Grid
        container
        className="flex-centered"
        sx={{ width: "100%", height: "100%" }}
      >
        {groups?.map((group: Group) => (
          <GroupCard key={group.id} group={group} />
        ))}
      </Grid>
    </div>
  );
};

export default GroupsGrid;
