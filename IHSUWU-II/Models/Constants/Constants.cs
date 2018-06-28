using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models.Constants
{
    public static class AppConstants
    {
        public static string BUSINESS_LAYER_POLICY = "BusinessLayerPolicy";
        public static string AUTHORIZATION_POLICY = "AuthorizationPolicy";
        public static string PRESENTATION_LAYER_POLICY = "PresentationLayerPolicy";
        public static string DATA_LAYER_POLICY = "DataLayerPolicy";
    }

    public enum FunctionKey
    {
        BackgroundProcessOverride = 0,

        //?????????????????????????????????

    }

    public static class SessionConstants
    {
        public static string SESSION_CONTEXT_INSTANCE = "Context";
        public static string CURRENT_LIST_DATE = "Current_list";

    }

    public static class ErrorCodes
    {
        public static readonly string[] AUTHORIZATION_FAILED = { "E0001", "User {0} does not have the function key: {1}." };

        public static readonly string AUTHENTICATION_FAILED = "Invalid username and/or password.";

        public static readonly string[] MATCHING_OUTGOING_CALL_NOT_FOUND = { "Error", "Could not find matching outgoing call" };

        #region Business ( 0 - 1000)

        public static readonly string[] UNKNOWN_ERROR = { "E000", "Unknown error occurred" };
        //public static readonly string[] AUTH_CREDENTIALS_NOT_PROVIDED = { "E001", "Authentication Credentials are not provided" };
        public static readonly string[] AUTH_CREDENTIALS_INVALID = { "E002", "Authentication Credentials are invalid" };
        public static readonly string[] UNAUTHORIZED_ACCESS = { "E003", "Unauthorized Access" };
        public static readonly string[] USER_DOESNOT_HAVE_PERMISSION = { "E004", "User doesn't have permission" };
        public static readonly string[] INVALID_AUTHENTICATION_CONTEXT = { "E005", "Invalid user context" };
        public static readonly string[] INVALID_DATA = { "E006", "Invalid data" };
        public static readonly string[] INVALID_CONFIGURATION = { "E007", "Invalid configuration" };
        public static readonly string[] SAVE_VEHICLE_IMAGE_FAILED = { "E008", "Saving new vehicle image failed" };
        public static readonly string[] SAVE_VEHICLE_MODEL_IMAGE_FAILED = { "E009", "Saving new vehicle model image failed" };
        public static readonly string[] SAVE_IMAGE_FAILED = { "E010", "Saving image failed" };
        public static readonly string[] ADD_VEHICLE_RESERVATION_FAILED = { "E011", "Adding reservation failed" };
        public static readonly string[] RESERVATION_CANCEL_FAILED = { "E012", "Reservation cancel failed. ReservationID={0}" };
        public static readonly string[] SAVE_VEHICLE_FAILED = { "E013", "Saving vehicle failed." };
        public static readonly string[] SAVE_RESERVATION_FAILED = { "E014", "Saving reservation failed" };
        public static readonly string[] INVALID_USER_PROFILE = { "E015", "Invalid user profile" };
        public static readonly string[] AGREEMENT_CANNOT_BE_DELETED = { "E016", "Agreement cannot be deleted." };
        public static readonly string[] AGREEMENT_CAN_BE_DELETED_CLOSE_STATUS = { "E017", "Agreement with status close cannot be deleted." };
        public static readonly string[] INVALID_AGREEMENT = { "E018", "Inavlid agreement. The agreement doesn't exist." };
        public static readonly string[] ERROR_VEHICLE_IMAGE_PATH_CONFIGURATION = { "E019", "Error in vehilce image path configuration." };
        public static readonly string[] DRIVING_LICENSE_VERIFICATION_FAILED = { "E020", "Driving licence verification failed." };
        public static readonly string[] READ_AGREEMENT_HISTORY_FAILED = { "E021", "Error occured when reading agreement history." };
        public static readonly string[] READ_AGREEMENT_HISTORY_LOG_FAILED = { "E022", "Error occured when reading a agreement history log." };
        public static readonly string[] INVALID_RESERVATION = { "E023", "Inavlid Reservation. The Reservation doesn't exist." };
        public static readonly string[] RESERVATION_CANNOT_BE_DELETED = { "E024", "Reservation cannot be deleted." };
        public static readonly string[] AGREEMENT_STATUS_CANNOT_BE_CHANGED = { "E025", "Agreement status cannot be Changed." };
        public static readonly string[] GET_USER_BY_LOCATION_TO_SEND_EMAIL_FAILED = { "E1298", "Error occured when retriving user emails by their location" };
        #endregion

        #region Data Access ( 1001 - 2000)

        public static readonly string[] INSERT_VEHICLE_FAILED = { "E1001", "Error occurred when insert fleet" };
        public static readonly string[] GET_VEHICLE_MAKE_FAILED = { "E1002", "Error occurred when retriving fleet make" };
        public static readonly string[] GET_ALL_VEHICLE_MAKE_FAILED = { "E1003", "Error occurred when retriving all fleet makes" };
        public static readonly string[] GET_VEHICLE_TYPE_FAILED = { "E1004", "Error occurred when retriving fleet type" };
        public static readonly string[] GET_ALL_VEHICLE_TYPE_FAILED = { "E1005", "Error occurred when retriving all fleet types" };
        public static readonly string[] GET_LOCATION_FAILED = { "E1006", "Error occurred when retriving location" };
        public static readonly string[] GET_ALL_LOCATION_FAILED = { "E1007", "Error occurred when retriving all locations" };
        public static readonly string[] GET_LOCATION_FOR_STOREID_FAILED = { "E1008", "Error occurred when retriving location for store id" };
        public static readonly string[] ADD_NEW_CUSTOMER_ERROR = { "E1009", "Error occurred when adding a new customer" };
        public static readonly string[] SERCH_RESERVATION_FAILED = { "E1010", "Error occurred when serching for reservations" };
        public static readonly string[] UPDATE_VEHICLE_FAILED = { "E1011", "Error occurred when updaing fleet" };
        public static readonly string[] GET_VEHICLE_FAILED = { "E1012", "Error occurred when retriving fleet.fleetID={0}" };
        public static readonly string[] SEARCH_VEHICLE_FAILED = { "E1013", "Error occurred when searching fleets" };
        public static readonly string[] GET_CUSTOMER_FAILED = { "E1014", "Error occurred when retriving customer" };
        public static readonly string[] GET_VEHICLE_IMAGE_FAILED = { "E1015", "Error occurred when retriving fleet image" };
        public static readonly string[] INSERT_VEHICLE_IMAGE_FAILED = { "E1016", "Error occurred when insert fleet image" };
        public static readonly string[] UPDATE_VEHICLE_IMAGE_FAILED = { "E1017", "Error occurred when update fleet image. fleetImageID={0}" };
        public static readonly string[] SEARCH_AVAILABLE_VEHICLES_FAILED = { "E1018", "Error occurred when searching available fleets" };
        public static readonly string[] GET_VEHICLE_MODEL_FAILED = { "E1019", "Error occurred when retriving fleet model" };
        public static readonly string[] GET_RESERVATION_FAILED = { "E1020", "Error occurred when retriving reservation information" };
        public static readonly string[] SEARCH_AGREEMENTS_FAILED = { "E1021", "Error occurred when searching agreements" };
        public static readonly string[] SEARCH_CUSTOMER_FAILED = { "E1022", "Error occurred when searching customer" };
        public static readonly string[] GET_LEASING_COMPANY_FAILED = { "E1023", "Error occurred when retriving leasing company" };
        public static readonly string[] GET_ALL_LEASING_COMPANY_FAILED = { "E1024", "Error occurred when retriving all leasing companies" };
        public static readonly string[] GET_INSURANCE_COMPANY_FAILED = { "E1025", "Error occurred when retriving insurance company" };
        public static readonly string[] GET_ALL_INSURANCE_COMPANY_FAILED = { "E1026", "Error occurred when retriving all insurance company" };
        public static readonly string[] GET_DRIVERS_FOR_AGREEMENT_FAILED = { "E1027", "Error occurred when retriving drivers for agreenent" };
        public static readonly string[] INSERT_NEW_DRIVER_FAILED = { "E1028", "Error occurred when insert driver" };
        public static readonly string[] UPDATE_DRIVER_FAILED = { "E1029", "Error occurred when update driver" };
        public static readonly string[] INSERT_LOCATION_FAILED = { "E1030", "Error occurred when insert location detail" };
        public static readonly string[] UPDATE_LOCATION_FAILED = { "E1031", "Error occurred when update location detail" };
        public static readonly string[] INSER_RATE_FAIL = { "E1032", "Error occurred when inserting rate detail" };
        public static readonly string[] UPDATE_RATE_FAILED = { "E1033", "Error occurred when update Rate detail" };
        public static readonly string[] GET_VEHICLE_MODEL_IMAGE_FAILED = { "E1034", "Error occurred when retriving fleet model image" };
        public static readonly string[] GET_VEHICLE_MODEL = { "E1035", "Error occurred when retriving fleet model" };
        public static readonly string[] UPDATE_VEHICLE_MAKE_FAILED = { "E1036", "Error occurred when updating fleet make. MakeID = {0}" };
        public static readonly string[] INSERT_VEHICLE_MAKE_FAILED = { "E1037", "Error occurred when inserting fleet make" };
        public static readonly string[] INSERT_VEHICLE_MODEL_FAILED = { "E1038", "Error occurred when inserting fleet model" };
        public static readonly string[] UPDATE_VEHICLE_MODEL_FAILED = { "E1039", "Error occurred when updating fleet model. ModelID = {0}" };
        public static readonly string[] INSERT_VEHICLE_EXPENSE_FAILED = { "E1040", "Error occurred when inserting fleet expense" };
        public static readonly string[] UPDATE_VEHICLE_EXPENSE_FAILED = { "E1041", "Error occurred when updating fleet expense" };
        public static readonly string[] GET_ALL_VEHICLE_EXPENSE_TYPE_FAILED = { "E1042", "Error occurred when retriving fleet expense type" };
        public static readonly string[] GET_ALL_VEHICLE_EXPENSE_FAILED = { "E1043", "Error occurred when retriving fleet expense" };
        public static readonly string[] INSERT_VEHICLE_MODEL_IMAGE_FAILED = { "E1044", "Error occurred when insert model image" };
        public static readonly string[] UPDATE_VEHICLE_MODEL_IMAGE_FAILED = { "E1045", "Error occurred when update model image. fleetModelImageID={0}" };
        public static readonly string[] GET_VEHICLE_MAKE = { "E1046", "Error occurred when retriving fleet make" };
        public static readonly string[] GET_APPLICATION_STATISTICS = { "E1047", "Error occurred when retriving Application Statistics" };
        public static readonly string[] GET_DRIVER_TYPES = { "E1048", "Error occurred when retriving Driver Types" };
        public static readonly string[] GET_AGREEMENT_FAILED = { "E1049", "Error occurred when retriving agreement" };
        public static readonly string[] GET_ALLYEAR_FAILED = { "E1286", "Error occurred when retriving years by make and model" };
        public static readonly string[] GET_ALLVEHICLE_FAILED = { "E1287", "Error occurred when retriving vehicles" };

        public static readonly string[] INSERT_AGREEMENT_NOTE_FAIL = { "E1050", "Error occurred when inserting agreement note" };
        public static readonly string[] GET_AGREEMENT_NOTE_HISTORY = { "E1051", "Error occurred when retriving agreement note history" };
        public static readonly string[] UPDATE_TAX_FAILED = { "E1052", "Error occurred when updating tax detail" };
        public static readonly string[] DELETE_TAX_FAILED = { "E1053", "Error occurred when deleting tax" };
        public static readonly string[] INSERT_TAX_FAILED = { "E1054", "Error occurred inserting new tax" };
        public static readonly string[] GET_TAX_FAILED = { "E1055", "Error occurred when retriving tax details" };
        public static readonly string[] GET_AGREEMENT_NOTE = { "E1056", "Error occurred when retriving agreement note" };
        public static readonly string[] UPDATE_MISCCHARGE_FAILED = { "E1057", "Error occurred when updating  Miscellaneous charge detail" };
        public static readonly string[] DELETE_MISCCHARGE_FAILED = { "E1058", "Error occurred when deleting  Miscellaneous charge" };
        public static readonly string[] INSERT_MISCCHARGE_FAILED = { "E1059", "Error occurred inserting new  Miscellaneous charge" };
        public static readonly string[] GET_MISCCHARGE_FAILED = { "E1060", "Error occurred when retriving  Miscellaneous charge details" };
        public static readonly string[] GET_MISCCHARGE_FOR_LOCATION_FAILED = { "E1061", "Error occurred when retriving misc chargers for location" };
        public static readonly string[] INSERT_RESERVATION_MISCCHARGE_FAILED = { "E1062", "Error occurred when insert reservation misc charge" };
        public static readonly string[] DELETE_RESERVATION_MISCCHARGE_FAILED = { "E1063", "Error occurred when delete reservation misc charge, ReservationID={0} MiscChargeID={1}" };
        public static readonly string[] GET_ALL_TAXED_BY_LOCATION_FAILED = { "E1064", "Error occurred when retriving taxes by location" };

        public static readonly string[] READ_TAX_FAILED = { "E1065", "Error occurred when retriving tax details" };
        public static readonly string[] READ_MISCCHARGE_FAILED = { "E1066", "Error occurred when retriving misc chargers" };

        public static readonly string[] GET_INSURENCE_DETAIL_FOR_AGREEMENT_FAILED = { "E1067", "Error occurred when retriving insurence detail for agreenent" };
        public static readonly string[] INSERT_AGREEMENT_INSURENCE_FAILED = { "E1068", "Error occurred when insert agreement insurence detail" };
        public static readonly string[] UPDATE_AGREEMENT_INSURENCE_FAILED = { "E1069", "Error occurred when update agreement insurence detail" };
        public static readonly string[] READ_AGREEMENT_INSURENCE_FAILED = { "E1070", "Error occurred when retriving agreement insurence details" };

        public static readonly string[] REMOVE_AGREEMENT_MISCCHARGE_FAILED = { "E1071", "Error occurred when deleting reservation misc charges for AgreementID={0}" };
        public static readonly string[] GET_LOCATIONS_FAILED = { "E1072", "Error occurred when retriving locations" };

        public static readonly string[] AGREEMENT_CHECKOUT_FAILED = { "E1073", "Error occurred when checkout agreement" };
        public static readonly string[] AGREEMENT_CHECKIN_FAILED = { "E1074", "Error occurred when checkin agreement" };
        public static readonly string[] SAVE_VEHICLE_FAILED_LICENSE_EXISTS = { "E1075", "License Number already exits." };
        public static readonly string[] SAVE_VEHICLE_FAILED_VIN_EXISTS = { "E1076", "VIN Number already exits." };
        public static readonly string[] SAVE_VEHICLE_MAKE_FAILED_NAME_EXISTS = { "E1077", "Vehicle make name already exists." };
        public static readonly string[] INSERT_VEHICLE_TYPE_FAILED = { "E1078", "Error occurred when inserting vehicle type." };
        public static readonly string[] SAVE_VEHICLE_TYPE_FAILED_NAME_EXISTS = { "E1079", "Vehicle type name already exists." };
        public static readonly string[] UPDATE_VEHICLE_TYPE_FAILED = { "E1080", "Error occurred when updating vehicle type. TypeID = {0}" };
        public static readonly string[] DELETE_RESERVATION_MISCCHARGE_BY_RESID_FAILED = { "E1081", "Error occurred when delete reservation misc charge, ReservationID={0}" };
        public static readonly string[] DELETE_RESERVATION_TAX_BY_RESID_FAILED = { "E1082", "Error occurred when delete reservation tax, ReservationID={0}" };
        public static readonly string[] GET_RATE_NAME_FOR_LOCATION_FAILED = { "E1083", "Error occurred when reading rate names" };
        public static readonly string[] READ_RATE_FAILED = { "E1084", "Error occurred when reading rate data from data reader" };
        public static readonly string[] GET_RATES_FOR_LOCATION_RATE_NAME_FAILED = { "E1085", "Error occurred when retriving rates for location and rate name" };

        //Referral Errors
        public static readonly string[] GET_REFERRAL_NAMES_FAILED = { "E1086", "Error occurred when retriving referral names" };
        public static readonly string[] GET_REFERRAL_DETAIL_FAILED = { "E1087", "Error occurred when retriving referral details" };
        public static readonly string[] GET_FEATURE_FAILED = { "E1088", "Error occured when retrieving feature for client" };
        public static readonly string[] SAVE_VEHICLE_DAMAGE_FAILED = { "E1089", "Error occurred when inserting vehicle Damage" };
        public static readonly string[] DELETE_VEHICLE_DAMAGE_BY_VEHI_ID_FAILED = { "E1090", "Error occured when deleting vehicle damage detail" };
        public static readonly string[] GET_ALL_VEHICLE_DAMAGE_FAILED = { "E1091", "Error occured when retrieving vehicle damage detail" };

        public static readonly string[] DELETE_AGREEMENT_FAILED = { "E1092", "Error occured when deleting contract." };
        public static readonly string[] AGREEMENT_DOES_NOT_EXIST = { "E1093", "contract does not exist." };
        public static readonly string[] CANCEL_RESERVATION_FAILED = { "E1094", "Error occurred when canceling reservation. ReservationID={0}" };

        public static readonly string[] GET_TRAFFIC_TICKET_FAILED = { "E1095", "Error occurred when retriving traffic tickets.TrafficTicketId={0}" };
        public static readonly string[] UPDATE_TRAFFIC_TICKET_FAILED = { "E1096", "Error occurred when update traffic ticket detail.TrafficTicketId={0}" };
        public static readonly string[] INSERT_TRAFFIC_TICKET_FAILED = { "E1097", "Error occurred when insert traffic ticket detail" };
        public static readonly string[] UPDATE_VEHICLE_STATUS_FAILED = { "E1098", "Error occurred when update vehicle status for vehice Id : {0}" };

        public static readonly string[] GET_VEHICLES_BY_VEHICLE_TYPE_FAILED = { "E1099", "Error occurred when getting vehicles for vehice type Id : {0} for Location Id : {1}" };

        public static readonly string[] GET_LEASING_COMPANYS_FAILED = { "E1100", "Error occurred when retriving leasing Companies" };
        public static readonly string[] UPDATE_LEASING_COMPANY_FAILED = { "E1101", "Error occurred when update leasing Company detail" };
        public static readonly string[] INSERT_LEASING_COMPANY_FAILED = { "E1102", "Error occurred when insert leasing Company detail" };
        public static readonly string[] GET_LEASING_COMPANYS_BY_ID_FAILED = { "E1103", "Error occurred when retriving leasing Company by id" };

        public static readonly string[] GET_INSURANCE_COMPANYS_FAILED = { "E1104", "Error occurred when retriving insurance Companies" };
        public static readonly string[] UPDATE_INSURANCE_COMPANY_FAILED = { "E1105", "Error occurred when update insurance Company detail" };
        public static readonly string[] INSERT_INSURANCE_COMPANY_FAILED = { "E1106", "Error occurred when insert insurance Company detail" };
        public static readonly string[] GET_INSURANCE_COMPANYS_BY_ID_FAILED = { "E1107", "Error occurred when retriving insurance Company by id" };

        public static readonly string[] EXCHANGE_VEHICLE_FAILED = { "E1108", "Error occurred when exchanging vehicle for contract Id : {0}" };
        public static readonly string[] GET_VEHICLE_EXCHANGES_FAILED = { "E1109", "Error occurred when gettting vehicle exchanges for contract Id : {0}" };

        public static readonly string[] GET_USERS_FAILED = { "E1110", "Error occurred when retriving users" };
        public static readonly string[] UPDATE_USER_FAILED = { "E1111", "Error occurred when update user detail" };
        public static readonly string[] INSERT_USER_FAILED = { "E1112", "Error occurred when insert user detail" };
        public static readonly string[] GET_USER_BY_ID_FAILED = { "E1113", "Error occurred when retriving user by id" };
        public static readonly string[] INSERT_MAX_USER_FAILED = { "E1114", "Error occurred when inserting maximum user" };
        public static readonly string[] GET_USER_ROLE_FAILED = { "E1115", "Error occurred when retriving user role name" };

        public static readonly string[] GET_REFERRAL_FAILED = { "E1116", "Error occurred when retriving referrals detail" };
        public static readonly string[] UPDATE_REFERRAL_FAILED = { "E1117", "Error occurred when update referral detail" };
        public static readonly string[] INSERT_REFERRAL_FAILED = { "E1118", "Error occurred when insert referral detail" };

        public static readonly string[] GET_REFERRAL_BY_ID_FAILED = { "E1119", "Error occurred when retriving referral detail by id" };
        public static readonly string[] DELETE_AGREEMENT_NOTE_FAILED = { "E1120", "Error occurred when deleting Note" };
        public static readonly string[] UPDATE_AGREEMENT_NOTE_FAILED = { "E1121", "Error occurred when updating Note" };

        public static readonly string[] GET_INVOICE_FAILED = { "E1122", "Error occurred when retriving invoice detail" };
        public static readonly string[] UPDATE_INVOICE_FAILED = { "E1123", "Error occurred when update invoice detail" };
        public static readonly string[] INSERT_INVOICE_FAILED = { "E1124", "Error occurred when insert invoice detail" };
        public static readonly string[] DELETE_INVOICE_FAILED = { "E1125", "Error occurred when deleting invoice detail" };

        public static readonly string[] GET_PAYMENT_FAILED = { "E1126", "Error occurred when retriving payment detail" };
        public static readonly string[] UPDATE_PAYMENT_FAILED = { "E1127", "Error occurred when update payment detail" };
        public static readonly string[] INSERT_PAYMENT_FAILED = { "E1128", "Error occurred when insert payment detail" };
        public static readonly string[] DELETE_PAYMENT_FAILED = { "E1129", "Error occurred when deleting payment detail" };

        public static readonly string[] UPDATE_RESERVATION_FAILED = { "E1030", "Error occurred when updaing reservation. ReservationID={0}" };
        public static readonly string[] READ_VEHICLE_OPTION_FAILED = { "E1031", "Error occurred when reading vehicle options" };
        public static readonly string[] GET_ALL_VEHICLE_OPTIONS_FAILED = { "E1032", "Error occurred when retriving all vehicle options" };
        public static readonly string[] DELETE_ALL_VEHICLE_OPTIONS_FAILED = { "E1033", "Error occurred when deleting all vehicle options for VehicleID={0}" };
        public static readonly string[] INSERT_VEHICLE_OPTIONS_MAPPING_FAILED = { "E1034", "Error occurred when inserting vehicle options for VehicleID={0} VehicleOptionID={1}" };
        public static readonly string[] DELETE_VEHICLE_OPTIONS_MAPPING_FAILED = { "E1035", "Error occurred when deleting vehicle options for VehicleID={0} VehicleOptionID={1}" };

        public static readonly string[] INSERT_RESERVATION_TAX_FAILED = { "E1036", "Error occurred when insert reservation tax" };

        public static readonly string[] INSERT_AGREEMENT_TAX_FAILED = { "E1037", "Error occurred when insert contract tax" };
        public static readonly string[] INSERT_AGREEMENT_MISCCHARGE_FAILED = { "E1038", "Error occurred when insert contract misc charge" };
        public static readonly string[] DELETE_AGREEMENT_MISCCHARGE_FAILED = { "E1039", "Error occurred when delete contract misc charge, contractID={0} MiscChargeID={1}" };
        public static readonly string[] INSERT_BILLING_INFO_FAIL = { "E1140", "Error occurred when insert contract billing information" };
        public static readonly string[] UPDATE_BILLING_INFO_FAIL = { "E1141", "Error occurred when update contract billing information" };

        public static readonly string[] INSERT_COMPANY_EXPENSE_FAIL = { "E1142", "Error occurred when inserting company expense" };
        public static readonly string[] INSERT_COMPANY_EXPENSE_ITEM_FAIL = { "E1143", "Error occurred when inserting company expense item" };
        public static readonly string[] UPDATE_COMPANY_EXPENSE_FAILED = { "E1144", "Error occurred when updating company expense item" };
        public static readonly string[] UPDATE_COMPANY_EXPENSE_ITEM_FAILED = { "E1145", "Error occurred when updating company expense item" };
        public static readonly string[] GET_COMPANY_EXPENSE_FAILED = { "E1146", "Error occurred when retriving company expense detail" };
        public static readonly string[] DELETE_COMPANY_EXPENSE_FAILED = { "E1147", "Error occurred when deleting company expense detail" };
        public static readonly string[] DELETE_VEHICLE_MAKE_FAILED = { "E1148", "Error occurred when deleting vehicle make" };
        public static readonly string[] DELETE_VEHICLE_MODEL_FAILED = { "E1149", "Error occurred when deleting vehicle model" };
        public static readonly string[] SAVE_VEHICLE_MODEL_FAILED_NAME_EXISTS = { "E1150", "Vehicle model already exists." };
        public static readonly string[] DELETE_VEHICLE_MAKE_FAILED_RECORDS_EXISTS = { "E1151", "Cannot delete vehicle make \"{0}\"! It is being used by vehicles" };
        public static readonly string[] DELETE_VEHICLE_MODEL_FAILED_RECORDS_EXISTS = { "E1152", "Cannot delete vehicle model \"{0}\"! It is being used by vehicles" };
        public static readonly string[] DELETE_VEHICLE_TYPE_FAILED_RECORDS_EXISTS = { "E1153", "Cannot delete vehicle type \"{0}\"! It is being used by vehicles" };
        public static readonly string[] DELETE_VEHICLE_TYPE_FAILED = { "E1154", "Error occurred when deleting vehicle type" };
        public static readonly string[] GET_VEHICLE_OPTION_BY_ID_FAILED = { "E1155", "Error occurred when retrieving vehicle option" };
        public static readonly string[] INSERT_VEHICLE_OPTION_FAILED = { "E1156", "Error occurred when inserting vehicle option" };
        public static readonly string[] SAVE_VEHICLE_OPTION_FAILED_NAME_EXISTS = { "E1157", "vehicle option already exists" };
        public static readonly string[] UPDATE_VEHICLE_OPTION_FAILED = { "E1158", "Error occurred when updating vehicle option. OptionID = {0}" };
        public static readonly string[] DELETE_VEHICLE_OPTION_FAILED_RECORDS_EXISTS = { "E1159", "Cannot delete vehicle option \"{0}\"! It is being used by vehicles" };
        public static readonly string[] DELETE_VEHICLE_OPTION_FAILED = { "E1160", "Error occurred when deleting vehicle option" };
        public static readonly string[] ADD_NEW_ADDRESS_ERROR = { "E1161", "Error occurred when adding address" };
        public static readonly string[] UPDATE_ADDRESS_ERROR = { "E1162", "Error occurred when updating address" };
        public static readonly string[] ADD_NEW_CREDITCARD_ERROR = { "E1163", "Error occurred when adding credit card" };
        public static readonly string[] UPDATE_CREDITCARD_ERROR = { "E1164", "Error occurred when updating credit card" };
        public static readonly string[] SEARCH_CUSTOMER_ADDRESS_FAILED = { "E1165", "Error occurred when retreiving customer addresses" };
        public static readonly string[] DELETE_CUSTOMER_ADDRESS_FAILED = { "E1166", "Error occurred when deleting customer address" };

        public static readonly string[] SEARCH_CUSTOMER_CREDITCARD_FAILED = { "E1167", "Error occurred when retreiving customer credit cards" };
        public static readonly string[] DELETE_CUSTOMER_CREDITCARD_FAILED = { "E1168", "Error occurred when deleting customer credit card" };
        //  ----------------------
        public static readonly string[] GET_ALL_STORE_HOUR_FAILED = { "E1169", "Error occurred when retrieving all store hours" };

        public static readonly string[] GET_STORE_HOURS_BY_DAY_FAILED = { "E1170", "Error occurred when retrieving store hours by day" };
        public static readonly string[] INSERT_MISCCHARGE_OPTION_FAILED = { "E1171", "Error occurred when inserting miscallaneous charge option" };

        public static readonly string[] UPDATE_RESERVATION_MODULE_FAILED = { "E1172", "Error occurred when updating reservation module" };
        public static readonly string[] GET_COMPANY_EXPENSE_ITEM_FAILED = { "E1173", "Error occurred when retrieving company expense item" };

        public static readonly string[] GET_MISCCHARGE_OPTION_FAILED = { "E1174", "Error occurred when retrieving updating miscallaneous charge option" };

        public static readonly string[] DELETE_MISCCHARGE_OPTION_FAILED = { "E1175", "Error occurred when deleting miscallaneous charge option" };
        public static readonly string[] DELETE_COMPANY_EXPENSE_ITEM_FAILED = { "E1176", "Error occurred when deleting company expense item" };
        public static readonly string[] GET_RESERVATION_MODULE_FAILED = { "E1177", "Error occurred when retrieving reservation module" };
        public static readonly string[] GET_NEXT_OIL_CHANGE_FAILED = { "E1178", "Error occurred when retrieving next oil change" };
        public static readonly string[] GET_RATE_TYPES_FAILED = { "E1179", "Error occurred when retrieving rate types" };
        public static readonly string[] DELETE_INVOICE_ITEM_FAILED = { "E1180", "Error occurred when deleting invoice item" };
        public static readonly string[] CHECK_AGREEMENT_NO_EXIST_FAILED = { "E1181", "Error occurred when checking if contract number exist" };
        public static readonly string[] GET_AUTO_AGREEMENT_NO_FAILED = { "E1182", "Error occurred when getting auto contract number" };

        public static readonly string[] GET_INVOICE_ITEM_FAILED = { "E1183", "Error occurred when retrieving invoice item" };
        public static readonly string[] INSERT_INVOICE_ITEM_FAILED = { "E1184", "Error occurred when inserting invoice item" };
        public static readonly string[] UPDATE_INVOICE_ITEM_FAILED = { "E1185", "Error occurred when updating invoice item" };
        public static readonly string[] INSERT_AGREEMENT_FAILED = { "E1186", "Error occurred when adding contract" };

        public static readonly string[] UPDATE_AGREEMENT_FAILED = { "E1187", "Error occurred when updating contract" };
        public static readonly string[] INSERT_VEHICLE_EXCHANGE = { "E1188", "Error occurred when inserting vehicle exchange" };
        public static readonly string[] GET_CHECK_OUT_LOCATION_FAILED = { "E1189", "Error occurred when retrieving check out location" };
        public static readonly string[] GET_CHECK_IN_LOCATION_FAILED = { "E1190", "Error occurred when retrieving check in location" };
        public static readonly string[] GET_RETURN_LOCATION_FAILED = { "E1191", "Error occurred when retrieving return location" };
        public static readonly string[] GET_RATE_FAILED = { "E1192", "Error occurred when retrieving rate" };
        public static readonly string[] READ_BILLING_INFO_FAILED = { "E1193", "Error occurred when retrieving billing information" };

        public static readonly string[] UPDATE_TOLL_DETAIL_FAILED = { "E1194", "Error occurred when updating toll detail" };
        public static readonly string[] GET_CHART_DATASOURCE_FAILED = { "E1195", "Error occurred when retrieving chart data source" };

        public static readonly string[] GET_FEATURE_ATTRIBUTE_FAILED = { "E1196", "Error occurred when retrieving feature attribute" };
        public static readonly string[] INSERT_TOLL_HEADER_FAILED = { "E1197", "Error occurred when inserting toll header" };

        public static readonly string[] INSERT_CLIENT_FAILED = { "E1198", "Error occurred when inserting client" };
        public static readonly string[] UPDATE_CLIENT_FAILED = { "E1199", "Error occurred when updating client" };

        public static readonly string[] GET_CLIENT_FAILED = { "E1200", "Error occurred when retrieving client" };
        public static readonly string[] INSERT_TOLL_DETAIL = { "E1201", "Error occurred when inserting toll detail" };
        public static readonly string[] UPDATE_CUSTOMER_ERROR = { "E1202", "Error occurred when updating customer" };

        public static readonly string[] ADD_DRIVER_LICENSE_CHECK_FAILED = { "E1203", "Error occurred when adding driver license check" };
        public static readonly string[] GET_LOCATION_FOR_STATEID_FAILED = { "E1204", "Error occurred when retreiving locations for the state" };
        public static readonly string[] GET_STATES_FAILED = { "E1205", "Error occurred when retrieving states" };
        public static readonly string[] GET_COUNTRIES_FAILED = { "E1206", "Error occurred when retrieving countries" };

        public static readonly string[] GET_STATE_FAILED = { "E1207", "Error occurred when retrieving state" };
        public static readonly string[] GET_COUNTRY_FAILED = { "E1208", "Error occurred when retrieving country" };
        public static readonly string[] DELETE_RATE_FAILED = { "E1209", "Error occurred when deleting rate" };
        public static readonly string[] GET_RATE_NAMES_BY_CLIENT_ID_FAILED = { "E1210", "Error occurred when retrieving rate names by client id" };
        public static readonly string[] INSERT_RESERVATION_FAILED = { "E1211", "Error occurred when inserting reservation" };
        public static readonly string[] GET_LEASE_EXPIRE_FAILED = { "E1212", "Error occurred when retrieving lease expiry" };
        public static readonly string[] VEHICLE_NO_EXIST_CHECK_FAILED = { "E1213", "Error occurred when checking for vehicle number" };
        public static readonly string[] GET_VEHICLE_MODELS_FAILED = { "E1214", "Error occurred when retrieving vehicle models" };
        public static readonly string[] GET_VEHICLE_SUMMARY_FAILED = { "E1215", "Error occurred when retrieving vehicle summary" };
        public static readonly string[] GET_VEHICLE_TYPE_IMAGE_FAILED = { "E1216", "Error occurred when retrieving vehicle type image" };
        public static readonly string[] UPDATE_MISCCHARGE_OPTION_FAILED = { "E1217", "Error occurred when updating miscallaneous charge option" };
        public static readonly string[] GET_RATE_DETAILS_FAILED = { "E1218", "Error occurred when retrieving rate details" };
        public static readonly string[] GET_TOLL_REPORT_FAILED = { "E1219", "Error occurred when retrieving toll report" };
        public static readonly string[] DELETE_CLIENT_FEATURE_FAILED = { "E1220", "Error occurred when deleting client feature" };
        public static readonly string[] UPDATE_CLIENT_FEATURE_FAILED = { "E1221", "Error occurred when updating client feature" };

        public static readonly string[] ADD_NEW_CLIENT_FEATURE_FAILED = { "E1222", "Error occurred when adding new client feature" };
        public static readonly string[] GET_CLIENT_DOCS_FAILED = { "E1223", "Error occurred when retrieving client documents" };
        public static readonly string[] ADD_CLIENT_DOC_FAILED = { "E1224", "Error occurred when adding client document" };
        public static readonly string[] GET_USED_FILE_SIZE_FAILED = { "E1225", "Error occurred when retrieving used file size" };
        public static readonly string[] DELETE_CLIENT_DOCUMENT_FAILED = { "E1226", "Error occurred when deleting client document" };
        public static readonly string[] GET_MAX_FILE_SIZE_FAILED = { "E1227", "Error occurred when retrieving max file size" };

        public static readonly string[] ADD_SHARED_LOCATION_FAILED = { "E1228", "Error occurred when adding shared location" };
        public static readonly string[] DELETE_ALL_SHARED_LOCATIONS_FAILED = { "E1229", "Error occurred when deleting all shared locations" };
        public static readonly string[] GET_LOCATION_BY_VEHICLE_FAILED = { "E1230", "Error occurred when retriving location by vehicle" };
        public static readonly string[] GET_COMBINED_LOCATIONS_BY_LOCATION_ID = { "E1231", "Error occurred when retriving combined locations by vehicle" };

        public static readonly string[] INSERT_VEHICLE_FAILED_VEHICLE_NO_EXISTS = { "E1232", "Vehicle Number already exits." };

        public static readonly string[] GET_ALL_EQUIPMENT_TYPE_FAILED = { "E1233", "Error occured when get all equipment type." };
        public static readonly string[] GET_ALL_EQUIPMENT_FAILED = { "E1234", "Error occured when get contract equipment ." };
        public static readonly string[] GET_ALL_EQUIPMENT_BY_ID_FAILED = { "E1235", "Error occured when get equipment by id." };
        public static readonly string[] ADD_EQUIPMENT_FAILED = { "E1236", "Error occured when inserting new equipment." };
        public static readonly string[] EDIT_EQUIPMENT_FAILED = { "E1236", "Error occured when editing equipment." };
        public static readonly string[] DELETE_EQUIPMENT_FAILED = { "E1236", "Error occured when deleting equipment." };

        public static readonly string[] GET_ALL_SEASON_RATE_FAILED = { "E1237", "Error occured when get all season rate." };

        public static readonly string[] GET_SEASON_RATE_BY_ID_FAILED = { "E1238", "Error occured when get season rate by id." };
        public static readonly string[] ADD_SEASON_RATE_FAILED = { "E1239", "Error occured when inserting new season rate." };
        public static readonly string[] EDIT_SEASON_RATE_FAILED = { "E1240", "Error occured when editing season rate." };
        public static readonly string[] DELETE_SEASON_RATE_FAILED = { "E1241", "Error occured when deleting season rate." };
        public static readonly string[] GET_AGREEMENT_HISTORY_FAILED = { "E1242", "Error occured when getting contract history." };
        public static readonly string[] GET_AGREEMENT_HISTORY_LOG_FAILED = { "E1243", "Error occured when getting a contract history log." };

        public static readonly string[] GET_ALL_VEHICLE_TRACK_FAILED = { "E1242", "Error occured when get all vehicle track." };

        public static readonly string[] GET_VEHICLE_TRACK_BY_ID_FAILED = { "E1243", "Error occured when get vehicle track by id." };
        public static readonly string[] ADD_VEHICLE_TRACK_FAILED = { "E1244", "Error occured when inserting new vehicle track." };
        public static readonly string[] EDIT_VEHICLE_TRACK_FAILED = { "E1245", "Error occured when editing vehicle track." };
        public static readonly string[] DELETE_VEHICLE_TRACK_FAILED = { "E1246", "Error occured when deleting vehicle track." };

        public static readonly string[] FILL_DROP_DOWN_LIST_FAILED = { "E1247", "Error occurred when filling the drop down list" };
        public static readonly string[] GET_REPORT_CONFIGURATION_FAILED = { "E1248", "Error occurred when retriving report configuration" };
        public static readonly string[] EXECUTE_REPORT_CONFIGURATION = { "E1249", "Error occurred when executing report configuration" };
        public static readonly string[] GET_REPORT_FAILED = { "E1250", "Error occurred when retriving report" };

        public static readonly string[] SEARCH_RESERVATION_FAILED = { "E1251", "Error occurred when search reservation" };

        public static readonly string[] GET_VEHICLE_NOTE_HISTORY = { "E1252", "Error occurred when retriving report" };
        public static readonly string[] DELETE_VEHICLE_NOTE_FAILED = { "E1253", "Error occurred when deleting note" };
        public static readonly string[] UPDATE_VEHICLE_NOTE_FAILED = { "E1254", "Error occurred when updating Note" };
        public static readonly string[] INSERT_VEHICLE_NOTE_FAIL = { "E1255", "Error occurred when inserting vehicle note" };

        public static readonly string[] GET_CUSTOMER_NOTE_HISTORY = { "E1256", "Error occurred when retriving note" };
        public static readonly string[] DELETE_CUSTOMER_NOTE_FAILED = { "E1257", "Error occurred when deleting note" };
        public static readonly string[] UPDATE_CUSTOMER_NOTE_FAILED = { "E1258", "Error occurred when updating note" };
        public static readonly string[] INSERT_CUSTOMER_NOTE_FAIL = { "E1259", "Error occurred when inserting vehicle note" };

        public static readonly string[] GET_CUSTOMINVOICE_FAILED = { "E1260", "Error occurred when retriving Custom invoice detail" };
        public static readonly string[] DELETE_RESERVATION_FAILED = { "E1261", "Error occured when deleting reservation." };
        public static readonly string[] GET_DOCUMENTS_FAILED = { "E1261", "Error occured when getting documents." };
        public static readonly string[] NO_PERMISSION_TOGET_DOCUMENTS = { "E1261", "Error occured when getting documents." };
        public static readonly string[] GET_MAPPED_CLIENT_ID_FAILED = { "E1262", "Error occurred when retriving mapped client id" };

        public static readonly string[] INSERT_DROPOFFCHARGE_FAILED = { "E1263", "Error occured when trying to insert the dropoff charge record." };
        public static readonly string[] DELETE_DROPOFFCHARGE_FAILED = { "E1264", "Error occured when deleting the record." };
        public static readonly string[] UPDATE_DROPOFFCHARGE_FAILED = { "E1264", "Error occured when updating the record." };


        public static readonly string[] GET_VEHICLE_RESERVATION_FEATURES_FAILED = { "E1265", "Error occured when getting reservation features." };
        public static readonly string[] GET_ALL_VEHICLE_RESERVATION_FEATURES_FAILED = { "E1266", "Error occured when getting all reservation features." };
        public static readonly string[] UPDATE_VEHICLE_RESERVATION_FEATURES_FAILED = { "E1267", "Error occured when updating reservation features." };
        public static readonly string[] INSERT_VEHICLE_RESERVATION_FEATURES_FAILED = { "E1268", "Error occured when inserting reservation features." };
        public static readonly string[] DELETE_VEHICLE_RESERVATION_FEATURES_FAILED = { "E1269", "Error occured when deleting reservation features." };

        //ku
        public static readonly string[] GET_AGREEMENTCHARGE_FEATURES_FAILED = { "E1270", "Error occured when getting agreement charge features." };
        public static readonly string[] GET_ALL_AGREEMENTCHARGE_FEATURES_FAILED = { "E1271", "Error occured when getting all agreement charge features." };
        public static readonly string[] UPDATE_AGREEMENTCHARGE_FEATURES_FAILED = { "E1272", "Error occured when updating agreement charge features." };
        public static readonly string[] INSERT_AGREEMENTCHARGE_FEATURES_FAILED = { "E1273", "Error occured when inserting agreement charge features." };
        public static readonly string[] DELETE_AGREEMENTCHARGE_FEATURES_FAILED = { "E1274", "Error occured when deleting agreement charge features." };
        public static readonly string[] GET_AGREEMENTCHARGETYPE_FEATURES_FAILED = { "E1275", "Error occured when getting agreement charge type features." };
        public static readonly string[] UPDATE_FINE_PAYMENT_FAILED = { "E1276", "Error occurred when update Fine Payment(Trafic Ticket) detail" };

        // Promotion Name
        public static readonly string[] INSERT_PROMOTIONCODENAME_FAILED = { "E1278", "Error occured when trying to insert the promo Code Name record." };
        public static readonly string[] GET_PROMOTIONCODENAME_NAMES_BY_CLIENT_ID_FAILED = { "E1279", "Error occurred when retrieving Promotion Code names by client id" };

        //Promotions Add
        public static readonly string[] INSERT_PROMOTION_FAILED = { "E1280", "Error occured when trying to insert the Promotions record." };
        public static readonly string[] UPDATE_PROMOTION_FAILED = { "E1281", "Error occured when updating the Promotions record." };
        public static readonly string[] DELETE_PROMOTION_FAILED = { "E1282", "Error occured when deleting the Promotions record." };
        public static readonly string[] GET_PROMOTIONS_NAMES_BY_CLIENT_ID_FAILED = { "E1283", "Error occurred when retrieving Promotions names by client id" };
        public static readonly string[] GET_PROMOTIONS_FAILED = { "E1284", "Error occurred when retrieving Promotions names by client id" };
        public static readonly string[] GET_PROMOTIONS_BYVEHICLEID_FAILED = { "E1285", "Error occurred when retrieving Promotions names by Vehicle ID" };
        //deposit
        public static readonly string[] GET_DEPOSIT_FAILED = { "E1286", "Error occurred when retriving deposit detail" };
        public static readonly string[] UPDATE_DEPOSIT_FAILED = { "E1287", "Error occurred when update deposit detail" };
        public static readonly string[] INSERT_DEPOSIT_FAILED = { "E1288", "Error occurred when insert deposit detail" };
        public static readonly string[] DELETE_DEPOSIT_FAILED = { "E1289", "Error occurred when deleting deposit detail" };

        public static readonly string[] DELETE_AGREEMENT_TAX_FAILED = { "E1290", "Error occured when deleting agreementTax." };
        public static readonly string[] DELETE_AGREEMENT_MISCHARGE_FAILED = { "E1291", "Error occured when deleting agreementMisCharge." };
        public static readonly string[] DELETE_RESERVATION_TAX_FAILED = { "E1292", "Error occured when deleting reservationTax." };
        public static readonly string[] DELETE_RESERVATION_MISCHARGE_FAILED = { "E1293", "Error occured when deleting reservationMisCharge." };

        //update reservation misc charge...
        public static readonly string[] UPDATE_RESERVATION_MISCHARGE_FAILED = { "E1294", "Error occured when updating reservationMisCharge." };
        //Custom reports
        public static readonly string[] GET_CUSTOM_SUMMARY_REPORTS = { "E1295", "Error occured when retriving custom summary report." };

        //
        public static readonly string[] GET_SEQUENCE_NUMBER_FAILED = { "E1296", "Error occurred when retring sequence number" };
        public static readonly string[] UPDATE_SEQUENCE_NUMBER_FAILED = { "E1297", "Error occured when updating sequence number." };
        public static readonly string[] GET_PAYMENT_GATEWAY_DETAILS_FAILED = { "E1298", "Error occured when retriving payment gateway details." };

        //service Type
        public static readonly string[] GET_ALL_SERVICE_TYPES_FAILED = { "E1299", "Error occured when retriving all service type details." };
        public static readonly string[] INSERT_SERVICE_TYPES_FAILED = { "E1300", "Error occured when inserting service type details." };
        public static readonly string[] UPDATE_SERVICE_TYPES_FAILED = { "E1301", "Error occured when updating service type details." };
        public static readonly string[] GET_SERVICE_TYPES_FAILED = { "E1302", "Error occured when retriving the service type details. " };
        public static readonly string[] DELETE_SERVICE_TYPES_FAILED = { "E1303", "Error occured when deleting service type details. " };

        //Vendor 
        public static readonly string[] GET_ALL_VENDOR_FAILED = { "E1304", "Error occured when retriving all vendor details." };
        public static readonly string[] INSERT_VENDOR_FAILED = { "E1305", "Error occured when inserting vendor details." };
        public static readonly string[] UPDATE_VENDOR_FAILED = { "E1306", "Error occured when updating vendor details." };
        public static readonly string[] GET_VENDOR_FAILED = { "E1307", "Error occured when the vendor details " };
        public static readonly string[] DELETE_VENDOR_FAILED = { "E1308", "Error occured when deleting vendor details " };

        //Authorized Service Employee
        public static readonly string[] GET_ALL_AUTHORIZED_EMPLOYEES_FAILED = { "E1309", "Error occured when retriving all authorized employee details." };
        public static readonly string[] INSERT_AUTHORIZED_EMPLOYEES_FAILED = { "E1310", "Error occured when inserting authorized employee details. " };
        public static readonly string[] UPDATE_AUTHORIZED_EMPLOYEES_FAILED = { "E1311", "Error occured when updating authorized employee details." };
        public static readonly string[] GET_AUTHORIZED_EMPLOYEES_BY_ID_FAILED = { "E1312", "Error occured when retriving authorized employee details. " };
        public static readonly string[] DELETE_AUTHORIZED_EMPLOYEES_FAILED = { "E1313", "Error occured when deleting authorized employee details." };

        //pmServiceSetUp
        public static readonly string[] GET_ALL_PM_SERVICESETUP_FAILED = { "E1314", "Error occured when retriving all pm service set up details." };
        public static readonly string[] INSERT_PM_SERVICESETUP_FAILED = { "E1315", "Error occured when inserting pm service set up details. " };
        public static readonly string[] UPDATE_PM_SERVICESETUP_FAILED = { "E1316", "Error occured when updating pm service set up details." };
        public static readonly string[] GET_PM_SERVICESETUP_BY_ID_FAILED = { "E1317", "Error occured when retriving pm service set up details. " };
        public static readonly string[] DELETE_PM_SERVICESETUP_FAILED = { "E1318", "Error occured when deleting pm service set up details." };

        //ServiceTypeItem  SERVICE_TYPE_ITEM
        public static readonly string[] GET_ALL_SERVICE_TYPE_ITEM_FAILED = { "E1319", "Error occured when retriving allServiceTypeItem details." };
        public static readonly string[] INSERT_SERVICE_TYPE_ITEM_FAILED = { "E1320", "Error occured when inserting ServiceTypeItem details. " };
        public static readonly string[] UPDATE_SERVICE_TYPE_ITEM_FAILED = { "E1321", "Error occured when updating ServiceTypeItem details." };
        public static readonly string[] GET_SERVICE_TYPE_ITEM_BY_ID_FAILED = { "E1322", "Error occured when retriving ServiceTypeItem details. " };
        public static readonly string[] DELETE_SERVICE_TYPE_ITEM_FAILED = { "E1323", "Error occured when deleting ServiceTypeItem details." };

        // vehicle maintenance service
        public static readonly string[] GET_ALL_VEHICLE_MAINTENANCE_SERVICE_FAILED = { "E1324", "Error occured when retriving all vehicle maintenance service details." };
        public static readonly string[] INSERT_VEHICLE_MAINTENANCE_SERVICE_FAILED = { "E1325", "Error occured when inserting vehicle maintenance service details." };
        public static readonly string[] UPDATE_VEHICLE_MAINTENANCE_SERVICE_FAILED = { "E1326", "Error occured when updatingvehicle maintenance service details." };
        public static readonly string[] GET_VEHICLE_MAINTENANCE_SERVICE_BY_ID_FAILED = { "E1327", "Error occured when the vehicle maintenance service details " };
        public static readonly string[] DELETE_VEHICLE_MAINTENANCE_SERVICE_FAILED = { "E1328", "Error occured when deleting vehicle maintenance service details " };

        // serviceItem froservice
        public static readonly string[] GET_ALL_SERVICE_ITEM_FOR_PM_SERVICE_SERVICE_FAILED = { "E1324", "Error occured when retriving all vehicle maintenance service details." };
        public static readonly string[] INSERT_SERVICE_ITEM_FOR_PM_SERVICE_SERVICE_FAILED = { "E1325", "Error occured when inserting service item for Pm service" };
        public static readonly string[] UPDATE_SERVICE_ITEM_FOR_PM_SERVICE_SERVICE_FAILED = { "E1326", "Error occured when updatingvehicle maintenance service details." };

        public static readonly string[] GET_ALL_PM_SERVICE_BY_VEHICLEID_FAILED = { "E1327", "Error occured when get all pm service details for vehicle." };
        public static readonly string[] GET_ALL_PM_SERVICE_BY_PMID_FAILED = { "E1327", "Error occured when get all pm service details for pm id." };
        public static readonly string[] GET_ALL_TYPE_FAILED = { "E1328", "Error occurred when retriving all types." };

        //Payement GatewayDetails
        public static readonly string[] INSERT_PAYMENTGATEWAYDETAILS_FAILED = { "E1329", "Error occured when Inserting payment gateway details." };
        public static readonly string[] EDIT_PAYMENTGATEWAYDETAILS_FAILED = { "E1330", "Error occured when Updating payment gateway details." };

        public static readonly string[] GET_RATES_FOR_CALCULATIONS_FAILED = { "E1331", "Error occurred when retrieving rates for base calculations" };
        public static readonly string[] INSERT_RATES_FOR_CALCULATIONS_FAILED = { "E1332", "Error occured when inserting  rates for base calculations" };
        public static readonly string[] UPDATE_RATES_FOR_CALCULATIONS_FAILED = { "E1333", "Error occured when updating Rates for base calculations" };
        public static readonly string[] DELETE_RATES_FOR_CALCULATIONS_FAILED = { "E1334", "Error occured when deleting rates for base calculations" };

        public static readonly string[] GET_ALL_CLIENT_APP_SETTING_BY_CLIENTID_FAILED = { "E1335", "Error occured when retriving clientAppSetting" };

        #endregion

    }
}