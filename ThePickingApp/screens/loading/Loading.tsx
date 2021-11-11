import * as React from "react";
import { useState } from "react";
import { Alert, StyleSheet, TextInput, TouchableOpacity, View } from "react-native";
import { Text } from '../../components/Themed';
import { adminAPI, AllAppData, RootStackScreenProps } from "../../types";


export default function Loading() {
    return (<Text style={styles.loading}>Carregando...</Text>);
}


const styles = StyleSheet.create({
    loading: {
        fontSize: 18,
        width: '100%',
        textAlign: 'center'
    }
});