import { UserState } from "./user.state";

export const userFeatureKey = 'user';

export interface UserRootState {
    [userFeatureKey]: UserState;
}
