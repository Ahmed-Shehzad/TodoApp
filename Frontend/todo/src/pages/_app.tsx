import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers-pro/AdapterDayjs";
import { LicenseInfo } from "@mui/x-license-pro";
import "bootstrap/dist/css/bootstrap.min.css";
import type { AppProps } from "next/app";

LicenseInfo.setLicenseKey("YOUR_LICENSE_KEY");

export default function App({ Component, pageProps }: AppProps) {
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Component {...pageProps} />
    </LocalizationProvider>
  );
}
