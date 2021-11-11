/**
 * Learn more about using TypeScript with React Navigation:
 * https://reactnavigation.org/docs/typescript/
 */

import { CompositeScreenProps, NavigatorScreenParams } from '@react-navigation/native';
import { NativeStackScreenProps } from '@react-navigation/native-stack';

declare global {
  namespace ReactNavigation {
    interface RootParamList extends RootStackParamList { }
  }
}

export type AllAppData = {
  user?: { Id: string, Name: string },

  dataObject?: any
  dataArray?: any[]
}

// export class AllAppData {
//   user?: { Id: string, Name: string };
//   data?: any
// }


export type RootStackParamList = {
  Root: undefined;
  StockList: AllAppData;
  Login: undefined;
  PickingHome: AllAppData;
  Picking: AllAppData;
};

export type RootStackScreenProps<Screen extends keyof RootStackParamList> = NativeStackScreenProps<
  RootStackParamList,
  Screen
>

export const adminAPI = "http://tpsadmin.azurewebsites.net";
export const pickingAPI = "http://tpspicking.azurewebsites.net";
export const warehouseAPI = "http://tpswarehouse.azurewebsites.net";

