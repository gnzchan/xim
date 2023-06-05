import JoinRoom from "./Room/JoinRoom";
import ListRoom from "./Room/ListRoom";
import CreateRoom from "./Room/CreateRoom";
import { selectActiveTab } from "../../app/store/common/commonSlice";
import { useSelector } from "react-redux";

const Home = () => {
  const activeTab = useSelector(selectActiveTab);

  return (
    <>
      {
        {
          join: <JoinRoom />,
          create: <CreateRoom />,
          rooms: <ListRoom />,
        }[activeTab]
      }
    </>
  );
};

export default Home;
