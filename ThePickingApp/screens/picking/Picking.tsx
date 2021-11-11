import * as React from "react";
import { FlatList, StyleSheet, TouchableOpacity, View } from "react-native";
import { Text } from '../../components/Themed';
import { pickingAPI, RootStackScreenProps } from "../../types";
import Loading from "../loading/Loading";

export default function Picking({ navigation, route }: RootStackScreenProps<'Picking'>) {

    const { user, dataObject } = route.params;
    const stocks = dataObject.stocks;
    const stock = dataObject.stock;

    const [orderpicking, setOrderPicking] = React.useState(dataObject.orderpicking);
    const [isLoading, setIsLoading] = React.useState(false);


    const Body = () => {
        return (
            <View style={styles.header}>
                <Text style={styles.title}>Order Picking</Text>
                <Text style={styles.orderpicking}>{orderpicking.Id} - {orderpicking.Description}</Text>

                <Text style={styles.label}>Itens</Text>
                <FlatList
                    style={styles.list}
                    data={Data(orderpicking.Items)}
                    keyExtractor={({ id }, index) => id}
                    renderItem={({ item }) => Item({ navigation, item })}
                />
            </View>
        );
    }


    return (
        <View style={styles.header}>
            {isLoading ? <Loading /> : <Body></Body>}
            <View style={styles.footer}>
                <TouchableOpacity onPress={() => navigation.replace('PickingHome', { user, dataObject: { stocks, stock } })} style={[styles.button, styles.bgBlue]}>
                    <Text style={styles.buttonText}>Voltar</Text>
                </TouchableOpacity>
                {/* <TouchableOpacity onPress={() => navigation.replace('Login')} style={[styles.button, styles.bgRed]}>
                    <Text style={styles.buttonText}>Cancelar</Text>
                </TouchableOpacity> */}
            </View>
        </View>
    );
}

const Data = (items: any[]) => {
    return items.map((item: { Id: string, SKU: string, Description: string }) => {
        return { id: 'bd7acbea-c1b1-46c2-aed5-3ad53abb28ba-' + item.Id, sku: item.SKU, description: item.Description }
    });
}

const Item = ({ item }: any) => {
    return (
        <View style={styles.item}>
            <Text style={styles.itemText}>{item.sku}</Text>
            <Text style={styles.itemText}>{item.description}</Text>
        </View>
    );
}


const styles = StyleSheet.create({
    header: {
        flex: 1,
        justifyContent: 'flex-start',
        alignItems: 'flex-start',
        paddingVertical: 5,
        paddingHorizontal: 10,
        width: '100%'
    },
    orderpicking: {
        width: '100%',
        fontSize: 18,
        marginBottom: 20,
        paddingHorizontal: 10,
        paddingVertical: 10,
        backgroundColor: 'grey',
        fontWeight: 'bold',
    },
    title: {
        fontSize: 20,
        fontWeight: 'bold',
        marginTop: 20,
        marginBottom: 20,
        textAlign: 'center',
        width: '100%'
    },
    label: {
        fontSize: 16,
        paddingHorizontal: 5,
        width: '100%',
        marginBottom: 3
    },
    list: {
        width: '100%'
    },
    item: {
        alignItems: 'flex-start',
        paddingHorizontal: 5,
        paddingVertical: 5,
        marginBottom: 1,
        width: '100%',
        borderWidth: 1
    },
    itemText: {
        fontSize: 18
    },


    footer: {
        width: '100%',
        paddingVertical: 20,
        paddingHorizontal: 5,
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center'
    },
    button: {
        paddingVertical: 10,
        borderRadius: 4,
        width: '30%'
    },
    bgRed: {
        backgroundColor: 'red',
    },
    bgBlue: {
        backgroundColor: 'blue',
    },
    buttonText: {
        fontSize: 16,
        color: 'white',
        width: '100%',
        textAlign: 'center'
    },
});