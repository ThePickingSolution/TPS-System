import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import * as React from 'react';
import { ColorSchemeName } from 'react-native';
import AppHeader from '../screens/header/AppHeader';
import Login from '../screens/login/Login';
import PickingHome from '../screens/picking-home/PickingHome';
import Picking from '../screens/picking/Picking';
import RootPage from '../screens/root/RootPage';
import StockList from '../screens/stock-list/StockList';
import { RootStackParamList } from '../types';

export default function Navigation({ colorScheme }: { colorScheme: ColorSchemeName }) {
    return (
        <NavigationContainer>
            <RootNavigator />
        </NavigationContainer>
    );
}

const Stack = createNativeStackNavigator<RootStackParamList>();
function RootNavigator() {
    return (
        <Stack.Navigator initialRouteName="Login" screenOptions={{ header: AppHeader }}>
            <Stack.Screen name="Root" component={RootPage} options={{ headerShown: true, title: 'The Picking App' }} />
            <Stack.Screen name="Login" component={Login} options={{ headerShown: true }} />
            <Stack.Screen name="StockList" component={StockList} options={{ headerShown: true }} />
            <Stack.Screen name="PickingHome" component={PickingHome} options={{ headerShown: true }} />
            <Stack.Screen name="Picking" component={Picking} options={{ headerShown: true }} />
        </Stack.Navigator>
    );
}