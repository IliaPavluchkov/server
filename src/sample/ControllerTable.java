package sample;

import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.stage.Stage;

import java.io.IOException;

public class ControllerTable {

    @FXML
    private Button Sort;

    @FXML
    private Button back;

    @FXML
    void initialize(){
        back.setOnAction(event -> {
            FXMLLoader loader=new FXMLLoader();
            loader.setLocation(getClass().getResource("/sample/sample.fxml"));

            try {
                loader.load();
            } catch (IOException e) {
                e.printStackTrace();
            }

            Parent par=loader.getRoot();
            Stage stage=new Stage();
            stage.setScene(new Scene(par));
            stage.showAndWait();
        });
        Sort.setOnAction(event -> {
            Sort.getScene().getWindow().hide();
            FXMLLoader loader2=new FXMLLoader();
            loader2.setLocation(getClass().getResource("/sample/clientTable.fxml"));

            try {
                loader2.load();
            } catch (IOException e) {
                e.printStackTrace();
            }

            Parent par2=loader2.getRoot();
            Stage stage=new Stage();
            stage.setScene(new Scene(par2));

            stage.showAndWait();

        });
    }

}
