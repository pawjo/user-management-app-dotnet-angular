import { UserState } from "./user.state";
import { userFeatureKey } from "./userRoot.state";

export interface AppState{
    [userFeatureKey]: UserState;
}