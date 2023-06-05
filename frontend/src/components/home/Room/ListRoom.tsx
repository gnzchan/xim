import { Box, Tab, Tabs } from "@mui/material";
import {
  useGetMyRoomsQuery,
  useGetMyAttendedRoomsQuery,
} from "../../../app/store/room/roomsApiSlice";
import { useSelector, useDispatch } from "react-redux";
import {
  setActiveRoomTab,
  selectActiveRoomTab,
} from "../../../app/store/common/commonSlice";
import RoomGrid from "./RoomGrid";

const ListRoom = () => {
  const { data: myRooms } = useGetMyRoomsQuery();
  const { data: myAttendedRooms } = useGetMyAttendedRoomsQuery();

  const dispatch = useDispatch();
  const activeRoomTab = useSelector(selectActiveRoomTab);

  const handleTabIndexChange = (
    event: React.SyntheticEvent,
    newTabIndex: number
  ) => {
    dispatch(setActiveRoomTab(newTabIndex));
  };

  return (
    <Box
      className="flex-centered"
      sx={{
        flexDirection: "column",
        width: "100%",
        height: "100%",
      }}
    >
      <Box
        sx={{ borderBottom: 1, borderColor: "divider", marginBlock: "10px" }}
      >
        <Tabs value={activeRoomTab} onChange={handleTabIndexChange}>
          <Tab label="Hosted" />
          <Tab label="Attended" />
        </Tabs>
      </Box>
      <Box
        className="flex-centered"
        sx={{
          flexDirection: "column",
          border: "1px solid red",
          height: "100%",
          width: "70%",
          minWidth: "300px",
        }}
      >
        {
          {
            0: <RoomGrid rooms={myRooms ?? []} />,
            1: <RoomGrid rooms={myAttendedRooms ?? []} />,
          }[activeRoomTab]
        }
      </Box>
    </Box>
  );
};

export default ListRoom;
