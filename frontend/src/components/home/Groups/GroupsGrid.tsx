import { Grid } from "@mui/material";
import { Group } from "../../../app/models/group";
import GroupCard from "./GroupCard";
import { useState, useEffect } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

interface Props {
  groups: Group[] | null;
}

const GroupsGrid = ({ groups }: Props) => {
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
