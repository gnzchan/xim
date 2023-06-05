import { Box } from "@mui/material";
import { Outlet } from "react-router-dom";

import { Provider } from "react-redux";
import { store } from "../app/store/store";
import { PersistGate } from "redux-persist/integration/react";
import { persistStore } from "redux-persist";

const persistor = persistStore(store);

function App() {
  return (
    <Provider store={store}>
      <PersistGate persistor={persistor}>
        <Box
          className="flex-centered"
          sx={{
            minHeight: "400px",
            height: "100vh",
          }}
        >
          <Outlet />
        </Box>
      </PersistGate>
    </Provider>
  );
}

export default App;
