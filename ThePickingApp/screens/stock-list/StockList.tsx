import * as React from "react";
import { useState } from "react";
import { Alert, FlatList, StyleSheet, TextInput, TouchableOpacity, View } from "react-native";
import { Text } from '../../components/Themed';
import { RootStackScreenProps, warehouseAPI } from "../../types";
import Loading from "../loading/Loading";




export default function StockList({ route, navigation }: RootStackScreenProps<'StockList'>) {
    const { user, dataObject } = route.params;

    const [stocks, setStocks] = useState(dataObject && dataObject.stocks ? dataObject.stocks : []);
    const [isLoading, setIsLoading] = useState(false);

    if (!user)
        navigation.replace('Login');

    React.useEffect(() => {
        if (stocks.length == 0) {
            setIsLoading(true);
            fetch(`${warehouseAPI}/api/sector`)
                .then(data => {
                    data.json().then(data => {
                        setStocks(data);
                        if (!data || data.length == 0) {
                            Alert.alert("Setores não Cadastrados", "Setores não Cadastrados");
                        }
                    });
                })
                .catch(err => {
                    Alert.alert("Erro", err && err.error && err.error.Message ? err.error.Message : 'Falha ao listar os setores');
                })
                .finally(() => setIsLoading(false))
        }
    }, []);

    const Data = (stocks: any[]) => {
        return stocks.map((stk: { Id: string, Name: string, Code: string }) => {
            return { id: 'bd7acbea-c1b1-46c2-aed5-3ad53abb28ba-' + stk.Id, title: stk.Name, stock: stk }
        });
    }

    const Item = ({ item }: any) => {

        const dataObject = {
            stocks: stocks,
            stock: item.stock
        }

        return (
            <View style={styles.itemContainer}>
                <TouchableOpacity onPress={() => navigation.replace('PickingHome', { user, dataObject })} style={styles.button}>
                    <Text style={styles.buttonText}>{item.title}</Text>
                </TouchableOpacity>
            </View>
        );
    }

    return (
        <View style={styles.container}>
            <Text style={styles.title}>Setores</Text>
            {!isLoading ? <FlatList
                data={Data(stocks)}
                keyExtractor={({ id }, index) => id}
                renderItem={({ item }) => Item({ item })}
            /> : <Loading></Loading>}
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        width: '100%'
    },
    title: {
        width: '100%',
        fontSize: 24,
        textAlign: 'center',
        fontWeight: 'bold',
        paddingTop: 30,
        paddingBottom: 20
    },
    itemContainer: {
        width: '100%',
        alignItems: 'center'
    },
    button: {
        backgroundColor: 'green',
        width: '90%',
        alignItems: 'flex-start',
        paddingVertical: 10,
        marginBottom: 5,
        borderRadius: 5
    },
    buttonText: {
        fontSize: 18,
        color: 'white',
        width: '100%',
        textAlign: 'left',
        paddingLeft: 10
    },
});