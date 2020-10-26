package sample;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.CheckBox;
import javafx.scene.control.ComboBox;
import javafx.scene.control.DatePicker;
import javafx.scene.control.RadioButton;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;

import java.io.IOException;

public class Controller {

    @FXML
    private AnchorPane dateOfBirthText;

    @FXML
    private TextField surnameText;

    @FXML
    private TextField nameText;

    @FXML
    private TextField middleNameText;

    @FXML
    private TextField passportSeriesText;

    @FXML
    private RadioButton genderM;

    @FXML
    private RadioButton genderG;

    @FXML
    private CheckBox militaryServiceYes;

    @FXML
    private ComboBox<?> cityOfActualResidenceBox;

    @FXML
    private ComboBox<?> familyStatusBox;

    @FXML
    private ComboBox<?> citizenshipBox;

    @FXML
    private ComboBox<?> disabilityBox;

    @FXML
    private Button butTable;

    @FXML
    private Button butSave;

    @FXML
    private CheckBox militaryServiceNo;

    @FXML
    private CheckBox retireeYes;

    @FXML
    private CheckBox retireeNo;

    @FXML
    private DatePicker dateOfBirthT;

    @FXML
    private TextField issuedByText;

    @FXML
    private TextField passportNumberText;

    @FXML
    private TextField ident_NumberText;

    @FXML
    private TextField placeOfBirthText;

    @FXML
    private TextField addressResidenceText;

    @FXML
    private DatePicker dateOfIssueT;

    @FXML
    private TextField housePhoneT;

    @FXML
    private TextField mobPhoneT;

    @FXML
    private TextField emailT;

    @FXML
    private TextField placeOfWorkT;

    @FXML
    private TextField positionT;

    @FXML
    void initialize() {
        butTable.setOnAction(event -> {
            butTable.getScene().getWindow().hide();

        });
    }
}
