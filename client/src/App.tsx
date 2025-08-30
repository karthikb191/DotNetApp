import { List, ListItem, Typography } from "@mui/material";
import axios from "axios"
import { useEffect, useState } from "react"

function App() {

  //useState is a react hooks that allows us to remember the state of something
  const[activities, SetActivities] = useState<Activity[]>([])

  // This should execute when the above component mounts. It will execute the code here.
  // It will execute once 
  useEffect(()=>{
    /*using javascript
    //Fetches a resource from the network
    const jsPromise = fetch("https://localhost:5001/api/activities")
    jsPromise
    .then(response => response.json())
    .then(data => SetActivities(data))*/

    const promise = axios.get<Activity[]>("https://localhost:5001/api/activities")
    promise.then(response => SetActivities(response.data))
  }, []);

  return (
    <>
      <Typography variant='h1'> Reactivities </Typography>
      <List>
        {
          activities.map((activity) =>(
            <ListItem key={activity.id}>{activity.title}</ListItem>
          ))
        }
      </List>
    </>
  )
}

export default App
