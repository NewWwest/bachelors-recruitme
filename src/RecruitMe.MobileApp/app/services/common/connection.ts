import {connectionType, getConnectionType} from "tns-core-modules/connectivity";

export default class ConnectionService {
    public static IsConnectedToNetwork() : boolean {
        switch (getConnectionType()) {
            case connectionType.wifi:
            case connectionType.mobile:
                return true;
            default:
                return false;
        }
    }
}